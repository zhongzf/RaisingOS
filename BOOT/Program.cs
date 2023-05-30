using System;
using Uefi;

unsafe class Program
{
  static void Main() { }

  [System.Runtime.RuntimeExport("EfiMain")]
  static long EfiMain(IntPtr imageHandle, EFI_SYSTEM_TABLE* systemTable)
  {
    string hello = "Hello World!";
    fixed (char* pHello = hello)
    {
      systemTable->ConOut->OutputString(systemTable->ConOut, pHello);
    }

    while (true) ;
  }
}
