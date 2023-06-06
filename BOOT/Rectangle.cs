using System.Runtime.InteropServices;

namespace BOOT
{
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct Rectangle
  {
    public Point point;
    public Size size;
  }
}
