using System.Runtime.InteropServices;

namespace Uefi
{
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
}