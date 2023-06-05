using System;
using System.Runtime;
using Uefi;

unsafe class Program
{
  static void Main() { }

  [RuntimeExport("EfiMain")]
  static EFI_STATUS EfiMain(EFI_HANDLE imageHandle, EFI_SYSTEM_TABLE* systemTable)
  {
    systemTable->ConOut->ClearScreen(systemTable->ConOut);
    EfiInit(systemTable);

    Shell();
    return (RETURN_STATUS)0;
  }

  public const int CommandMaxLength = 100;

  protected static EFI_SYSTEM_TABLE* _systemTable;
  protected static EFI_SIMPLE_POINTER_PROTOCOL* _sop;
  protected static EFI_GRAPHICS_OUTPUT_PROTOCOL* _gop;

  private static void EfiInit(EFI_SYSTEM_TABLE* systemTable)
  {
    _systemTable = systemTable;

    var sopBytes = stackalloc byte[] { 0x9a, 0x4f, 0x0, 0x90, 0x27, 0x3f, 0xc1, 0x4d };
    var EFI_SIMPLE_POINTER_PROTOCOL_GUID = new EFI_GUID(0x31878c87, 0xb75, 0x11d5, sopBytes);
    fixed (EFI_SIMPLE_POINTER_PROTOCOL** p = &_sop)
    {
      _systemTable->BootServices->LocateProtocol(&EFI_SIMPLE_POINTER_PROTOCOL_GUID, null, (void**)p);
    }

    var gopBytes = stackalloc byte[] { 0x96, 0xfb, 0x7a, 0xde, 0xd0, 0x80, 0x51, 0x6a };
    var EFI_GRAPHICS_OUTPUT_PROTOCOL_GUID = new EFI_GUID(0x9042a9de, 0x23dc, 0x4a38, gopBytes);
    fixed (EFI_GRAPHICS_OUTPUT_PROTOCOL** p = &_gop)
    {
      _systemTable->BootServices->LocateProtocol(&EFI_GRAPHICS_OUTPUT_PROTOCOL_GUID, null, (void**)p);
    }

    _systemTable->BootServices->SetWatchdogTimer(0, 0, 0, null);
  }

  public static void OutputChar(char c)
  {
    var s = stackalloc char[2];
    s[0] = c;
    _systemTable->ConOut->OutputString(_systemTable->ConOut, s);
  }

  public static void OutputString(string s)
  {
    fixed (char* p = s)
    {
      _systemTable->ConOut->OutputString(_systemTable->ConOut, p);
    }
  }

  public static char GetChar()
  {
    EFI_INPUT_KEY key;
    ulong index;

    _systemTable->BootServices->WaitForEvent(1, &(_systemTable->ConIn->WaitForKey), &index);
    while ((RETURN_STATUS)_systemTable->ConIn->ReadKeyStroke(_systemTable->ConIn, &key) > 0)
      ;

    return key.UnicodeChar;
  }

  public static int GetString(char* buf, int bufSize)
  {
    var i = 0;
    for (; i < bufSize - 1;)
    {
      buf[i] = GetChar();
      OutputChar(buf[i]);
      if (buf[i] == '\b')
      {
        i--;
        continue;
      }
      else if (buf[i] == '\r')
      {
        OutputChar('\n');
        break;
      }
      i++;
    }
    buf[i] = '\0';
    return i;
  }

  public static bool StringCompare(string s1, char* s2)
  {
    if (s2[s1.Length] != '\0')
    {
      return false;
    }
    fixed (char* p1 = s1)
    {
      for (int i = 0; i < s1.Length; i++)
      {
        if (p1[i] != s2[i])
        {
          return false;
        }
      }
    }
    return true;
  }

  public static void DrawPixel(uint x, uint y, EFI_GRAPHICS_OUTPUT_BLT_PIXEL color)
  {
  }


  public static void Shell()
  {
    var command = stackalloc char[CommandMaxLength];
    while (true)
    {
      OutputString("RaisingOS > ");

      if (GetString(command, CommandMaxLength) <= 0)
        continue;

      if (StringCompare("hello", command))
        OutputString("Hello UEFI!\r\n");
      else
        OutputString("Command not found.\r\n");
    }
  }
}
