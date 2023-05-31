using System.Runtime.InteropServices;

namespace Uefi
{
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

