using System.Runtime.InteropServices;

namespace BOOT.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Size
    {
        public uint Width;
        public uint Height;
    }
}
