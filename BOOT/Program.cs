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

  private static void EfiInit(EFI_SYSTEM_TABLE* systemTable)
  {
    _systemTable = systemTable;
    _systemTable->BootServices->SetWatchdogTimer(0, 0, 0, null);
  }

  public static void PutC(char c)
  {
    var s = stackalloc char[2];
    s[0] = c;
    _systemTable->ConOut->OutputString(_systemTable->ConOut, s);
  }

  public static void Puts(string s)
  {
    fixed (char* p = s)
    {
      _systemTable->ConOut->OutputString(_systemTable->ConOut, p);
    }
  }

  public static char Getc()
  {
    EFI_INPUT_KEY key;
    ulong waitidx;

    _systemTable->BootServices->WaitForEvent(1, &(_systemTable->ConIn->WaitForKey), &waitidx);
    while ((RETURN_STATUS)_systemTable->ConIn->ReadKeyStroke(_systemTable->ConIn, &key) > 0)
      ;

    return key.UnicodeChar;
  }

  public static int Gets(char* buf, int bufSize)
  {
    var i = 0;
    for (; i < bufSize - 1;)
    {
      buf[i] = Getc();
      PutC(buf[i]);
      if (buf[i] == '\b')
      {
        i--;
        continue;
      }
      else if (buf[i] == '\r')
      {
        PutC('\n');
        break;
      }
      i++;
    }
    buf[i] = '\0';
    return i;
  }

  public static bool StringCompare(string s1, char* s2)
  {
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
      Puts("RaisingOS > ");

      if (Gets(command, CommandMaxLength) <= 0)
        continue;

      if (StringCompare("hello", command))
        Puts("Hello UEFI!\r\n");
      else
        Puts("Command not found.\r\n");
    }
  }
}
