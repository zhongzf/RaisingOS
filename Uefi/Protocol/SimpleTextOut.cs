using System;
using System.Runtime.InteropServices;

namespace Uefi
{
  ///
  /// The SIMPLE_TEXT_OUTPUT protocol is used to control text-based output devices.
  /// It is the minimum required protocol for any handle supplied as the ConsoleOut
  /// or StandardError device. In addition, the minimum supported text mode of such
  /// devices is at least 80 x 25 characters.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL
  {
    /**
      Reset the text output device hardware and optionally run diagnostics

      @param  This                 The protocol instance pointer.
      @param  ExtendedVerification Driver may perform more exhaustive verification
                                   operation of the device during reset.

      @retval EFI_SUCCESS          The text output device was reset.
      @retval EFI_DEVICE_ERROR     The text output device is not functioning correctly and
                                   could not be reset.

    **/
    public readonly delegate* unmanaged<EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL*, bool, EFI_STATUS> Reset;

    /**
      Write a string to the output device.

      @param  This   The protocol instance pointer.
      @param  String The NULL-terminated string to be displayed on the output
                     device(s). All output devices must also support the Unicode
                     drawing character codes defined in this file.

      @retval EFI_SUCCESS             The string was output to the device.
      @retval EFI_DEVICE_ERROR        The device reported an error while attempting to output
                                      the text.
      @retval EFI_UNSUPPORTED         The output device's mode is not currently in a
                                      defined text mode.
      @retval EFI_WARN_UNKNOWN_GLYPH  This warning code indicates that some of the
                                      characters in the string could not be
                                      rendered and were skipped.

    **/
    public readonly delegate* unmanaged<EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL*, char*, EFI_STATUS> OutputString;
  }
}
