using System;
using System.Runtime.InteropServices;

namespace Uefi
{
  ///
  /// EFI System Table
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_SYSTEM_TABLE
  {
    ///
    /// The table header for the EFI System Table.
    ///
    public EFI_TABLE_HEADER Hdr;
    ///
    /// A pointer to a null terminated string that identifies the vendor
    /// that produces the system firmware for the platform.
    ///
    public char* FirmwareVendor;
    ///
    /// A firmware vendor specific value that identifies the revision
    /// of the system firmware for the platform.
    ///
    public uint FirmwareRevision;
    ///
    /// The handle for the active console input device. This handle must support
    /// EFI_SIMPLE_TEXT_INPUT_PROTOCOL and EFI_SIMPLE_TEXT_INPUT_EX_PROTOCOL.
    ///
    public EFI_HANDLE ConsoleInHandle;
    ///
    /// A pointer to the EFI_SIMPLE_TEXT_INPUT_PROTOCOL interface that is
    /// associated with ConsoleInHandle.
    ///
    public EFI_SIMPLE_TEXT_INPUT_PROTOCOL* ConIn;
    ///
    /// The handle for the active console output device.
    ///    
    public EFI_HANDLE ConsoleOutHandle;
    ///
    /// A pointer to the EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL interface
    /// that is associated with ConsoleOutHandle.
    ///
    public EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL* ConOut;
    ///
    /// The handle for the active standard error console device.
    /// This handle must support the EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL.
    ///
    public EFI_HANDLE StandardErrorHandle;
    ///
    /// A pointer to the EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL interface
    /// that is associated with StandardErrorHandle.
    ///
    public EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL* StdErr;
  }
}
