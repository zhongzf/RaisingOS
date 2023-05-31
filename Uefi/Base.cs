using System;
using System.Runtime.InteropServices;

namespace Uefi
{
  ///
  /// 128 bit buffer containing a unique identifier value.
  /// Unless otherwise specified, aligned on a 64 bit boundary.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct GUID
  {
    uint Data1;
    ushort Data2;
    ushort Data3;
    public fixed byte Data4[8];

    public static explicit operator GUID*(GUID guid) => &guid;

    public GUID(uint d1, ushort d2, ushort d3, byte d4_0, byte d4_1, byte d4_2, byte d4_3, byte d4_4, byte d4_5, byte d4_6, byte d4_7)
    {
      Data1 = d1;
      Data2 = d2;
      Data3 = d3;
      Data4[0] = d4_0;
      Data4[1] = d4_1;
      Data4[2] = d4_2;
      Data4[3] = d4_3;
      Data4[4] = d4_4;
      Data4[5] = d4_5;
      Data4[6] = d4_6;
      Data4[7] = d4_7;
    }
  }

  //
  // Status codes common to all execution phases
  //
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct RETURN_STATUS
  {
    ulong Value;

    public static implicit operator RETURN_STATUS(ulong value) => new RETURN_STATUS() { Value = value };
    public static implicit operator ulong(RETURN_STATUS value) => value.Value;
  }
}

