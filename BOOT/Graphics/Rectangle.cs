﻿using System.Runtime.InteropServices;

namespace BOOT.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Rectangle
    {
        public Point Point;
        public Size Size;
    }
}
