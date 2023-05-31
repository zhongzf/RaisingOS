using System;
using System.Runtime.InteropServices;

namespace Uefi
{
  ///
  /// 128-bit buffer containing a unique identifier value.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_GUID
  {
    GUID Value;

    public static implicit operator EFI_GUID(GUID value) => new EFI_GUID() { Value = value };
    public static implicit operator GUID(EFI_GUID value) => value.Value;
  }

  ///
  /// Function return status for EFI API.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_STATUS
  {
    RETURN_STATUS Value;

    public static implicit operator EFI_STATUS(RETURN_STATUS value) => new EFI_STATUS() { Value = value };
    public static implicit operator RETURN_STATUS(EFI_STATUS value) => value.Value;
  }

  ///
  /// A collection of related interfaces.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_HANDLE
  {
    void* Value;

    public static implicit operator EFI_HANDLE(void* value) => new EFI_HANDLE() { Value = value };
    public static implicit operator void*(EFI_HANDLE value) => value.Value;
  }

  ///
  /// Handle to an event structure.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_EVENT
  {
    void* Value;

    public static implicit operator EFI_EVENT(void* value) => new EFI_EVENT() { Value = value };
    public static implicit operator void*(EFI_EVENT value) => value.Value;
  }

  ///
  /// EFI Time Abstraction:
  ///  Year:       1900 - 9999
  ///  Month:      1 - 12
  ///  Day:        1 - 31
  ///  Hour:       0 - 23
  ///  Minute:     0 - 59
  ///  Second:     0 - 59
  ///  Nanosecond: 0 - 999,999,999
  ///  TimeZone:   -1440 to 1440 or 2047
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_TIME
  {
    public ushort Year;
    public byte Month;
    public byte Day;
    public byte Hour;
    public byte Minute;
    public byte Second;
    public byte Pad1;
    public uint Nanosecond;
    public short TimeZone;
    public byte Daylight;
    public byte Pad2;
  }
}