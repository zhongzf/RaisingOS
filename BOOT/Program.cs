using System;
using System.Runtime;
//using BOOT.Uefi;
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

    return (EFI_STATUS)(RETURN_STATUS)0;
  }

  private static void GUI()
  {
    #region Cursor
    #pragma warning disable format
    var cursor = stackalloc int[]
    {
            1,0,0,0,0,0,0,0,0,0,0,0,
            1,1,0,0,0,0,0,0,0,0,0,0,
            1,2,1,0,0,0,0,0,0,0,0,0,
            1,2,2,1,0,0,0,0,0,0,0,0,
            1,2,2,2,1,0,0,0,0,0,0,0,
            1,2,2,2,2,1,0,0,0,0,0,0,
            1,2,2,2,2,2,1,0,0,0,0,0,
            1,2,2,2,2,2,2,1,0,0,0,0,
            1,2,2,2,2,2,2,2,1,0,0,0,
            1,2,2,2,2,2,2,2,2,1,0,0,
            1,2,2,2,2,2,2,2,2,2,1,0,
            1,2,2,2,2,2,2,2,2,2,2,1,
            1,2,2,2,2,2,2,1,1,1,1,1,
            1,2,2,2,1,2,2,1,0,0,0,0,
            1,2,2,1,0,1,2,2,1,0,0,0,
            1,2,1,0,0,1,2,2,1,0,0,0,
            1,1,0,0,0,0,1,2,2,1,0,0,
            0,0,0,0,0,0,1,2,2,1,0,0,
            0,0,0,0,0,0,0,1,1,0,0,0
    };
    #pragma warning disable format
    #endregion

    GetFrameBuffer(out var fb, out var width, out var height);

    EFI_SIMPLE_POINTER_STATE sts;
    float MouseSpeed = 200;

    int CursorX = 0;
    int CursorY = 0;

    for (; ; )
    {
      _sop->GetState(_sop, &sts);

      CursorX = Clamp(CursorX + (int)((sts.RelativeMovementX / 65536f) * MouseSpeed), 0, (int)width);
      CursorY = Clamp(CursorY + (int)((sts.RelativeMovementY / 65536f) * MouseSpeed), 0, (int)height);

      DrawCursor(cursor, fb, width, height, CursorX, CursorY);
    }
  }

  public const int CommandMaxLength = 100;

  protected static EFI_SYSTEM_TABLE* _systemTable;
  protected static EFI_SIMPLE_POINTER_PROTOCOL* _sop;
  protected static EFI_GRAPHICS_OUTPUT_PROTOCOL* _gop;

  private static void EfiInit(EFI_SYSTEM_TABLE* systemTable)
  {
    _systemTable = systemTable;

    EFI_GUID EFI_SIMPLE_POINTER_PROTOCOL_GUID = /*EFI.EFI_SIMPLE_POINTER_PROTOCOL_GUID;//*/ new GUID(0x31878c87, 0xb75, 0x11d5, 0x9a, 0x4f, 0x0, 0x90, 0x27, 0x3f, 0xc1, 0x4d);
    fixed (EFI_SIMPLE_POINTER_PROTOCOL** p = &_sop)
    {
      _systemTable->BootServices->LocateProtocol(&EFI_SIMPLE_POINTER_PROTOCOL_GUID, null, (void**)p);
    }

    EFI_GUID EFI_GRAPHICS_OUTPUT_PROTOCOL_GUID = /*EFI.EFI_GRAPHICS_OUTPUT_PROTOCOL_GUID;//*/ new GUID(0x9042a9de, 0x23dc, 0x4a38, 0x96, 0xfb, 0x7a, 0xde, 0xd0, 0x80, 0x51, 0x6a);
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
    while ((RETURN_STATUS)(EFI_STATUS)_systemTable->ConIn->ReadKeyStroke(_systemTable->ConIn, &key) > 0)
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

  public static void Shell()
  {
    var command = stackalloc char[CommandMaxLength];
    while (true)
    {
      OutputString("RaisingOS > ");

      if (GetString(command, CommandMaxLength) <= 0)
        continue;

      if (StringCompare("hello", command))
      {
        OutputString("Hello UEFI!\r\n");
      }
      else if (StringCompare("gui", command))
      {
        GUI();
      }
      else
      {
        OutputString("Command not found.\r\n");
      }
    }
  }


  public static void DrawCursor(int* cursor, uint* fb, uint width, uint height, int x, int y)
  {
    for (int h = 0; h < 19; h++)
    {
      for (int w = 0; w < 12; w++)
      {
        if (cursor[h * 12 + w] == 1)
        {
          SetPixel(fb, width, height, w + x, h + y, 0);
        }
        if (cursor[h * 12 + w] == 2)
        {
          SetPixel(fb, width, height, w + x, h + y, 0xFFFFFFFF);
        }
      }
    }
  }

  public static void SetPixel(uint* fb, uint width, uint height, int x, int y, uint color)
  {
    Clamp(x, 0, (int)width);
    Clamp(y, 0, (int)height);
    fb[width * y + x] = color;
  }

  public static int Clamp(int value, int min, int max)
  {
    if (value < min) return min;
    if (value > max) return max;
    return value;
  }

  public static void GetFrameBuffer(out uint* fb, out uint width, out uint height)
  {
    fb = (uint*)(void*)(ulong)_gop->Mode->FrameBufferBase;

    width = _gop->Mode->Info->HorizontalResolution;
    height = _gop->Mode->Info->VerticalResolution;

    _sop->Mode->ResolutionX = width;
    _sop->Mode->ResolutionY = height;
  }
}
