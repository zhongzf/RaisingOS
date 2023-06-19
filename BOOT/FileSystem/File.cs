using BOOT.Graphics;
using System;
using System.Runtime.InteropServices;

namespace BOOT.FileSystem
{
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct File
  {
    public fixed char Name[16];

    public Rectangle Rectangle { get; set; }
    public bool IsHighlighted { get; set; }
  }
}
