using System;
using Uefi;

unsafe class Program
{
  static void Main() { }

  [System.Runtime.RuntimeExport("EfiMain")]
  static EFI_STATUS EfiMain(EFI_HANDLE imageHandle, EFI_SYSTEM_TABLE* systemTable)
  {
    string hello = "Hello World!";
    fixed (char* pHello = hello)
    {
      systemTable->ConOut->OutputString(systemTable->ConOut, pHello);
    }

    EFI_INPUT_KEY key;
    var str = stackalloc char[3];
    while (true)
    {
      systemTable->ConIn->ReadKeyStroke(systemTable->ConIn, &key);
      if (key.UnicodeChar != '\r')
      {
        str[0] = key.UnicodeChar;
        str[1] = '\0';
      }
      else
      {
        str[0] = '\r';
        str[1] = '\n';
        str[2] = '\0';
      }
      systemTable->ConOut->OutputString(systemTable->ConOut, str);
    }
  }
}
