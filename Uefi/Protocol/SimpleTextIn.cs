using System;
using System.Runtime.InteropServices;

namespace Uefi
{
  ///
  /// The keystroke information for the key that was pressed.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_INPUT_KEY
  {
    public ushort ScanCode;
    public char UnicodeChar;
  }

  ///
  /// The EFI_SIMPLE_TEXT_INPUT_PROTOCOL is used on the ConsoleIn device.
  /// It is the minimum required protocol for ConsoleIn.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_SIMPLE_TEXT_INPUT_PROTOCOL
  {
    /**
    Reset the input device and optionally run diagnostics

    @param  This                 Protocol instance pointer.
    @param  ExtendedVerification Driver may perform diagnostics on reset.

    @retval EFI_SUCCESS          The device was reset.
    @retval EFI_DEVICE_ERROR     The device is not functioning properly and could not be reset.

    **/
    public readonly delegate* unmanaged<EFI_SIMPLE_TEXT_INPUT_PROTOCOL*, bool, EFI_STATUS> Reset;
    /**
      Reads the next keystroke from the input device. The WaitForKey Event can
      be used to test for existence of a keystroke via WaitForEvent () call.

      @param  This  Protocol instance pointer.
      @param  Key   A pointer to a buffer that is filled in with the keystroke
                    information for the key that was pressed.

      @retval EFI_SUCCESS      The keystroke information was returned.
      @retval EFI_NOT_READY    There was no keystroke data available.
      @retval EFI_DEVICE_ERROR The keystroke information was not returned due to
                               hardware errors.

    **/
    public readonly delegate* unmanaged<EFI_SIMPLE_TEXT_INPUT_PROTOCOL*, EFI_INPUT_KEY*, EFI_STATUS> ReadKeyStroke;
    ///
    /// Event to use with WaitForEvent() to wait for a key to be available
    ///
    public EFI_EVENT WaitForKey;
  }
}
