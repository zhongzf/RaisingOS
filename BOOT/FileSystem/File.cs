using System;
using System.Runtime.InteropServices;

namespace BOOT.FileSystem
{
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct File
  {
    public fixed char Name[16];
  }
}
