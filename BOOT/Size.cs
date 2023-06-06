using System.Runtime.InteropServices;

namespace BOOT
{
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct Size
  {
    public uint width;
    public uint height;
  }
}
