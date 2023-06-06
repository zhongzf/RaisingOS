using System.Runtime.InteropServices;

namespace BOOT
{
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct Point
  {
    public uint x;
    public uint y;
  }
}
