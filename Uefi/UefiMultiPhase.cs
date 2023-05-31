using System;
using System.Runtime.InteropServices;

namespace Uefi
{
  ///
  /// Data structure that precedes all of the standard EFI table types.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_TABLE_HEADER
  {
    ///
    /// A 64-bit signature that identifies the type of table that follows.
    /// Unique signatures have been generated for the EFI System Table,
    /// the EFI Boot Services Table, and the EFI Runtime Services Table.
    ///
    public ulong Signature;
    ///
    /// The revision of the EFI Specification to which this table
    /// conforms. The upper 16 bits of this field contain the major
    /// revision value, and the lower 16 bits contain the minor revision
    /// value. The minor revision values are limited to the range of 00..99.
    ///
    public uint Revision;
    ///
    /// The size, in bytes, of the entire table including the EFI_TABLE_HEADER.
    ///
    public uint HeaderSize;
    ///
    /// The 32-bit CRC for the entire table. This value is computed by
    /// setting this field to 0, and computing the 32-bit CRC for HeaderSize bytes.
    ///
    public uint CRC32;
    ///
    /// Reserved field that must be set to 0.
    ///
    public uint Reserved;
  }

}
