using System.Runtime.InteropServices;

namespace Uefi;
/** @file
  The EFI_SIMPLE_NETWORK_PROTOCOL provides services to initialize a network interface,
  transmit packets, receive packets, and close a network interface.

  Basic network device abstraction.

  Rx    - Received
  Tx    - Transmit
  MCast - MultiCast
  ...

Copyright (c) 2006 - 2018, Intel Corporation. All rights reserved.<BR>
SPDX-License-Identifier: BSD-2-Clause-Patent

  @par Revision Reference:
  This Protocol is introduced in EFI Specification 1.10.

**/

// #ifndef __SIMPLE_NETWORK_H__
// #define __SIMPLE_NETWORK_H__

public unsafe partial class EFI
{
  public static EFI_GUID EFI_SIMPLE_NETWORK_PROTOCOL_GUID = new GUID(
      0xA19832B9, 0xAC25, 0x11D3, new byte[] { 0x9A, 0x2D, 0x00, 0x90, 0x27, 0x3F, 0xC1, 0x4D });

  // typedef struct _EFI_SIMPLE_NETWORK_PROTOCOL EFI_SIMPLE_NETWORK_PROTOCOL;
}

///
/// Protocol defined in EFI1.1.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct EFI_SIMPLE_NETWORK { EFI_SIMPLE_NETWORK_PROTOCOL Value; public static implicit operator EFI_SIMPLE_NETWORK(EFI_SIMPLE_NETWORK_PROTOCOL value) => new EFI_SIMPLE_NETWORK() { Value = value }; public static implicit operator EFI_SIMPLE_NETWORK_PROTOCOL(EFI_SIMPLE_NETWORK value) => value.Value; }

///
/// Simple Network Protocol data structures.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct EFI_NETWORK_STATISTICS
{
  ///
  /// Total number of frames received.  Includes frames with errors and
  /// dropped frames.
  ///
  public ulong RxTotalFrames;

  ///
  /// Number of valid frames received and copied into receive buffers.
  ///
  public ulong RxGoodFrames;

  ///
  /// Number of frames below the minimum length for the media.
  /// This would be <64 for ethernet.
  ///
  public ulong RxUndersizeFrames;

  ///
  /// Number of frames longer than the maxminum length for the
  /// media.  This would be >1500 for ethernet.
  ///
  public ulong RxOversizeFrames;

  ///
  /// Valid frames that were dropped because receive buffers were full.
  ///
  public ulong RxDroppedFrames;

  ///
  /// Number of valid unicast frames received and not dropped.
  ///
  public ulong RxUnicastFrames;

  ///
  /// Number of valid broadcast frames received and not dropped.
  ///
  public ulong RxBroadcastFrames;

  ///
  /// Number of valid mutlicast frames received and not dropped.
  ///
  public ulong RxMulticastFrames;

  ///
  /// Number of frames w/ CRC or alignment errors.
  ///
  public ulong RxCrcErrorFrames;

  ///
  /// Total number of bytes received.  Includes frames with errors
  /// and dropped frames.
  //
  public ulong RxTotalBytes;

  ///
  /// Transmit statistics.
  ///
  public ulong TxTotalFrames;
  public ulong TxGoodFrames;
  public ulong TxUndersizeFrames;
  public ulong TxOversizeFrames;
  public ulong TxDroppedFrames;
  public ulong TxUnicastFrames;
  public ulong TxBroadcastFrames;
  public ulong TxMulticastFrames;
  public ulong TxCrcErrorFrames;
  public ulong TxTotalBytes;

  ///
  /// Number of collisions detection on this subnet.
  ///
  public ulong Collisions;

  ///
  /// Number of frames destined for unsupported protocol.
  ///
  public ulong UnsupportedProtocol;

  ///
  /// Number of valid frames received that were duplicated.
  ///
  public ulong RxDuplicatedFrames;

  ///
  /// Number of encrypted frames received that failed to decrypt.
  ///
  public ulong RxDecryptErrorFrames;

  ///
  /// Number of frames that failed to transmit after exceeding the retry limit.
  ///
  public ulong TxErrorFrames;

  ///
  /// Number of frames transmitted successfully after more than one attempt.
  ///
  public ulong TxRetryFrames;
}

///
/// The state of the network interface.
/// When an EFI_SIMPLE_NETWORK_PROTOCOL driver initializes a
/// network interface, the network interface is left in the EfiSimpleNetworkStopped state.
///
public enum EFI_SIMPLE_NETWORK_STATE
{
  EfiSimpleNetworkStopped,
  EfiSimpleNetworkStarted,
  EfiSimpleNetworkInitialized,
  EfiSimpleNetworkMaxState
}

public unsafe partial class EFI
{
  public const ulong EFI_SIMPLE_NETWORK_RECEIVE_UNICAST = 0x01;
  public const ulong EFI_SIMPLE_NETWORK_RECEIVE_MULTICAST = 0x02;
  public const ulong EFI_SIMPLE_NETWORK_RECEIVE_BROADCAST = 0x04;
  public const ulong EFI_SIMPLE_NETWORK_RECEIVE_PROMISCUOUS = 0x08;
  public const ulong EFI_SIMPLE_NETWORK_RECEIVE_PROMISCUOUS_MULTICAST = 0x10;

  public const ulong EFI_SIMPLE_NETWORK_RECEIVE_INTERRUPT = 0x01;
  public const ulong EFI_SIMPLE_NETWORK_TRANSMIT_INTERRUPT = 0x02;
  public const ulong EFI_SIMPLE_NETWORK_COMMAND_INTERRUPT = 0x04;
  public const ulong EFI_SIMPLE_NETWORK_SOFTWARE_INTERRUPT = 0x08;
}

public const ulong MAX_MCAST_FILTER_CNT = 16;
[StructLayout(LayoutKind.Sequential)]
public unsafe struct EFI_SIMPLE_NETWORK_MODE
{
  ///
  /// Reports the current state of the network interface.
  ///
  public uint State;
  ///
  /// The size, in bytes, of the network interface's HW address.
  ///
  public uint HwAddressSize;
  ///
  /// The size, in bytes, of the network interface's media header.
  ///
  public uint MediaHeaderSize;
  ///
  /// The maximum size, in bytes, of the packets supported by the network interface.
  ///
  public uint MaxPacketSize;
  ///
  /// The size, in bytes, of the NVRAM device attached to the network interface.
  ///
  public uint NvRamSize;
  ///
  /// The size that must be used for all NVRAM reads and writes. The
  /// start address for NVRAM read and write operations and the total
  /// length of those operations, must be a multiple of this value. The
  /// legal values for this field are 0, 1, 2, 4, and 8.
  ///
  public uint NvRamAccessSize;
  ///
  /// The multicast receive filter settings supported by the network interface.
  ///
  public uint ReceiveFilterMask;
  ///
  /// The current multicast receive filter settings.
  ///
  public uint ReceiveFilterSetting;
  ///
  /// The maximum number of multicast address receive filters supported by the driver.
  ///
  public uint MaxMCastFilterCount;
  ///
  /// The current number of multicast address receive filters.
  ///
  public uint MCastFilterCount;
  ///
  /// Array containing the addresses of the current multicast address receive filters.
  ///
  public fixed EFI_MAC_ADDRESS MCastFilter[MAX_MCAST_FILTER_CNT];
  ///
  /// The current HW MAC address for the network interface.
  ///
  public EFI_MAC_ADDRESS CurrentAddress;
  ///
  /// The current HW MAC address for broadcast packets.
  ///
  public EFI_MAC_ADDRESS BroadcastAddress;
  ///
  /// The permanent HW MAC address for the network interface.
  ///
  public EFI_MAC_ADDRESS PermanentAddress;
  ///
  /// The interface type of the network interface.
  ///
  public byte IfType;
  ///
  /// TRUE if the HW MAC address can be changed.
  ///
  public bool MacAddressChangeable;
  ///
  /// TRUE if the network interface can transmit more than one packet at a time.
  ///
  public bool MultipleTxSupported;
  ///
  /// TRUE if the presence of media can be determined; otherwise FALSE.
  ///
  public bool MediaPresentSupported;
  ///
  /// TRUE if media are connected to the network interface; otherwise FALSE.
  ///
  public bool MediaPresent;
}

//
// Protocol Member Functions
//

// /**
//   Changes the state of a network interface from "stopped" to "started".
// 
//   @param  This Protocol instance pointer.
// 
//   @retval EFI_SUCCESS           The network interface was started.
//   @retval EFI_ALREADY_STARTED   The network interface is already in the started state.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_START)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL  *This
//   );

// /**
//   Changes the state of a network interface from "started" to "stopped".
// 
//   @param  This Protocol instance pointer.
// 
//   @retval EFI_SUCCESS           The network interface was stopped.
//   @retval EFI_ALREADY_STARTED   The network interface is already in the stopped state.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_STOP)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL  *This
//   );

// /**
//   Resets a network adapter and allocates the transmit and receive buffers
//   required by the network interface; optionally, also requests allocation
//   of additional transmit and receive buffers.
// 
//   @param  This              The protocol instance pointer.
//   @param  ExtraRxBufferSize The size, in bytes, of the extra receive buffer space
//                             that the driver should allocate for the network interface.
//                             Some network interfaces will not be able to use the extra
//                             buffer, and the caller will not know if it is actually
//                             being used.
//   @param  ExtraTxBufferSize The size, in bytes, of the extra transmit buffer space
//                             that the driver should allocate for the network interface.
//                             Some network interfaces will not be able to use the extra
//                             buffer, and the caller will not know if it is actually
//                             being used.
// 
//   @retval EFI_SUCCESS           The network interface was initialized.
//   @retval EFI_NOT_STARTED       The network interface has not been started.
//   @retval EFI_OUT_OF_RESOURCES  There was not enough memory for the transmit and
//                                 receive buffers.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_INITIALIZE)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL                    *This,
//   IN ulong                                          ExtraRxBufferSize  OPTIONAL,
//   IN ulong                                          ExtraTxBufferSize  OPTIONAL
//   );

// /**
//   Resets a network adapter and re-initializes it with the parameters that were
//   provided in the previous call to Initialize().
// 
//   @param  This                 The protocol instance pointer.
//   @param  ExtendedVerification Indicates that the driver may perform a more
//                                exhaustive verification operation of the device
//                                during reset.
// 
//   @retval EFI_SUCCESS           The network interface was reset.
//   @retval EFI_NOT_STARTED       The network interface has not been started.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_RESET)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL   *This,
//   IN bool                       ExtendedVerification
//   );

// /**
//   Resets a network adapter and leaves it in a state that is safe for
//   another driver to initialize.
// 
//   @param  This Protocol instance pointer.
// 
//   @retval EFI_SUCCESS           The network interface was shutdown.
//   @retval EFI_NOT_STARTED       The network interface has not been started.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_SHUTDOWN)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL  *This
//   );

// /**
//   Manages the multicast receive filters of a network interface.
// 
//   @param  This             The protocol instance pointer.
//   @param  Enable           A bit mask of receive filters to enable on the network interface.
//   @param  Disable          A bit mask of receive filters to disable on the network interface.
//   @param  ResetMCastFilter Set to TRUE to reset the contents of the multicast receive
//                            filters on the network interface to their default values.
//   @param  McastFilterCnt   Number of multicast HW MAC addresses in the new
//                            MCastFilter list. This value must be less than or equal to
//                            the MCastFilterCnt field of EFI_SIMPLE_NETWORK_MODE. This
//                            field is optional if ResetMCastFilter is TRUE.
//   @param  MCastFilter      A pointer to a list of new multicast receive filter HW MAC
//                            addresses. This list will replace any existing multicast
//                            HW MAC address list. This field is optional if
//                            ResetMCastFilter is TRUE.
// 
//   @retval EFI_SUCCESS           The multicast receive filter list was updated.
//   @retval EFI_NOT_STARTED       The network interface has not been started.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_RECEIVE_FILTERS)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL                             *This,
//   IN uint                                                  Enable,
//   IN uint                                                  Disable,
//   IN bool                                                 ResetMCastFilter,
//   IN ulong                                                   MCastFilterCnt     OPTIONAL,
//   IN EFI_MAC_ADDRESS                                         *MCastFilter OPTIONAL
//   );

// /**
//   Modifies or resets the current station address, if supported.
// 
//   @param  This  The protocol instance pointer.
//   @param  Reset Flag used to reset the station address to the network interfaces
//                 permanent address.
//   @param  New   The new station address to be used for the network interface.
// 
//   @retval EFI_SUCCESS           The network interfaces station address was updated.
//   @retval EFI_NOT_STARTED       The network interface has not been started.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_STATION_ADDRESS)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL            *This,
//   IN bool                                Reset,
//   IN EFI_MAC_ADDRESS                        *New OPTIONAL
//   );

// /**
//   Resets or collects the statistics on a network interface.
// 
//   @param  This            Protocol instance pointer.
//   @param  Reset           Set to TRUE to reset the statistics for the network interface.
//   @param  StatisticsSize  On input the size, in bytes, of StatisticsTable. On
//                           output the size, in bytes, of the resulting table of
//                           statistics.
//   @param  StatisticsTable A pointer to the EFI_NETWORK_STATISTICS structure that
//                           contains the statistics.
// 
//   @retval EFI_SUCCESS           The statistics were collected from the network interface.
//   @retval EFI_NOT_STARTED       The network interface has not been started.
//   @retval EFI_BUFFER_TOO_SMALL  The Statistics buffer was too small. The current buffer
//                                 size needed to hold the statistics is returned in
//                                 StatisticsSize.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_STATISTICS)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL          *This,
//   IN bool                              Reset,
//   IN OUT ulong                            *StatisticsSize   OPTIONAL,
//   OUT EFI_NETWORK_STATISTICS              *StatisticsTable  OPTIONAL
//   );

// /**
//   Converts a multicast IP address to a multicast HW MAC address.
// 
//   @param  This The protocol instance pointer.
//   @param  IPv6 Set to TRUE if the multicast IP address is IPv6 [RFC 2460]. Set
//                to FALSE if the multicast IP address is IPv4 [RFC 791].
//   @param  IP   The multicast IP address that is to be converted to a multicast
//                HW MAC address.
//   @param  MAC  The multicast HW MAC address that is to be generated from IP.
// 
//   @retval EFI_SUCCESS           The multicast IP address was mapped to the multicast
//                                 HW MAC address.
//   @retval EFI_NOT_STARTED       The network interface has not been started.
//   @retval EFI_BUFFER_TOO_SMALL  The Statistics buffer was too small. The current buffer
//                                 size needed to hold the statistics is returned in
//                                 StatisticsSize.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_MCAST_IP_TO_MAC)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL          *This,
//   IN bool                              IPv6,
//   IN EFI_IP_ADDRESS                       *IP,
//   OUT EFI_MAC_ADDRESS                     *MAC
//   );

// /**
//   Performs read and write operations on the NVRAM device attached to a
//   network interface.
// 
//   @param  This       The protocol instance pointer.
//   @param  ReadWrite  TRUE for read operations, FALSE for write operations.
//   @param  Offset     Byte offset in the NVRAM device at which to start the read or
//                      write operation. This must be a multiple of NvRamAccessSize and
//                      less than NvRamSize.
//   @param  BufferSize The number of bytes to read or write from the NVRAM device.
//                      This must also be a multiple of NvramAccessSize.
//   @param  Buffer     A pointer to the data buffer.
// 
//   @retval EFI_SUCCESS           The NVRAM access was performed.
//   @retval EFI_NOT_STARTED       The network interface has not been started.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_NVDATA)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL          *This,
//   IN bool                              ReadWrite,
//   IN ulong                                Offset,
//   IN ulong                                BufferSize,
//   IN OUT void                             *Buffer
//   );

// /**
//   Reads the current interrupt status and recycled transmit buffer status from
//   a network interface.
// 
//   @param  This            The protocol instance pointer.
//   @param  InterruptStatus A pointer to the bit mask of the currently active interrupts
//                           If this is NULL, the interrupt status will not be read from
//                           the device. If this is not NULL, the interrupt status will
//                           be read from the device. When the  interrupt status is read,
//                           it will also be cleared. Clearing the transmit  interrupt
//                           does not empty the recycled transmit buffer array.
//   @param  TxBuf           Recycled transmit buffer address. The network interface will
//                           not transmit if its internal recycled transmit buffer array
//                           is full. Reading the transmit buffer does not clear the
//                           transmit interrupt. If this is NULL, then the transmit buffer
//                           status will not be read. If there are no transmit buffers to
//                           recycle and TxBuf is not NULL, * TxBuf will be set to NULL.
// 
//   @retval EFI_SUCCESS           The status of the network interface was retrieved.
//   @retval EFI_NOT_STARTED       The network interface has not been started.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_GET_STATUS)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL          *This,
//   OUT uint                              *InterruptStatus OPTIONAL,
//   OUT void                                **TxBuf OPTIONAL
//   );

// /**
//   Places a packet in the transmit queue of a network interface.
// 
//   @param  This       The protocol instance pointer.
//   @param  HeaderSize The size, in bytes, of the media header to be filled in by
//                      the Transmit() function. If HeaderSize is non-zero, then it
//                      must be equal to This->Mode->MediaHeaderSize and the DestAddr
//                      and Protocol parameters must not be NULL.
//   @param  BufferSize The size, in bytes, of the entire packet (media header and
//                      data) to be transmitted through the network interface.
//   @param  Buffer     A pointer to the packet (media header followed by data) to be
//                      transmitted. This parameter cannot be NULL. If HeaderSize is zero,
//                      then the media header in Buffer must already be filled in by the
//                      caller. If HeaderSize is non-zero, then the media header will be
//                      filled in by the Transmit() function.
//   @param  SrcAddr    The source HW MAC address. If HeaderSize is zero, then this parameter
//                      is ignored. If HeaderSize is non-zero and SrcAddr is NULL, then
//                      This->Mode->CurrentAddress is used for the source HW MAC address.
//   @param  DestAddr   The destination HW MAC address. If HeaderSize is zero, then this
//                      parameter is ignored.
//   @param  Protocol   The type of header to build. If HeaderSize is zero, then this
//                      parameter is ignored. See RFC 1700, section "Ether Types", for
//                      examples.
// 
//   @retval EFI_SUCCESS           The packet was placed on the transmit queue.
//   @retval EFI_NOT_STARTED       The network interface has not been started.
//   @retval EFI_NOT_READY         The network interface is too busy to accept this transmit request.
//   @retval EFI_BUFFER_TOO_SMALL  The BufferSize parameter is too small.
//   @retval EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_TRANSMIT)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL          *This,
//   IN ulong                                HeaderSize,
//   IN ulong                                BufferSize,
//   IN void                                 *Buffer,
//   IN EFI_MAC_ADDRESS                      *SrcAddr  OPTIONAL,
//   IN EFI_MAC_ADDRESS                      *DestAddr OPTIONAL,
//   IN ushort                               *Protocol OPTIONAL
//   );

// /**
//   Receives a packet from a network interface.
// 
//   @param  This       The protocol instance pointer.
//   @param  HeaderSize The size, in bytes, of the media header received on the network
//                      interface. If this parameter is NULL, then the media header size
//                      will not be returned.
//   @param  BufferSize On entry, the size, in bytes, of Buffer. On exit, the size, in
//                      bytes, of the packet that was received on the network interface.
//   @param  Buffer     A pointer to the data buffer to receive both the media header and
//                      the data.
//   @param  SrcAddr    The source HW MAC address. If this parameter is NULL, the
//                      HW MAC source address will not be extracted from the media
//                      header.
//   @param  DestAddr   The destination HW MAC address. If this parameter is NULL,
//                      the HW MAC destination address will not be extracted from the
//                      media header.
//   @param  Protocol   The media header type. If this parameter is NULL, then the
//                      protocol will not be extracted from the media header. See
//                      RFC 1700 section "Ether Types" for examples.
// 
//   @retval  EFI_SUCCESS           The received data was stored in Buffer, and BufferSize has
//                                  been updated to the number of bytes received.
//   @retval  EFI_NOT_STARTED       The network interface has not been started.
//   @retval  EFI_NOT_READY         The network interface is too busy to accept this transmit
//                                  request.
//   @retval  EFI_BUFFER_TOO_SMALL  The BufferSize parameter is too small.
//   @retval  EFI_INVALID_PARAMETER One or more of the parameters has an unsupported value.
//   @retval  EFI_DEVICE_ERROR      The command could not be sent to the network interface.
//   @retval  EFI_UNSUPPORTED       This function is not supported by the network interface.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_SIMPLE_NETWORK_RECEIVE)(
//   IN EFI_SIMPLE_NETWORK_PROTOCOL          *This,
//   OUT ulong                               *HeaderSize OPTIONAL,
//   IN OUT ulong                            *BufferSize,
//   OUT void                                *Buffer,
//   OUT EFI_MAC_ADDRESS                     *SrcAddr    OPTIONAL,
//   OUT EFI_MAC_ADDRESS                     *DestAddr   OPTIONAL,
//   OUT ushort                              *Protocol   OPTIONAL
//   );

public unsafe partial class EFI
{
  public const ulong EFI_SIMPLE_NETWORK_PROTOCOL_REVISION = 0x00010000;

  //
  // Revision defined in EFI1.1
  //
  public const ulong EFI_SIMPLE_NETWORK_INTERFACE_REVISION = EFI_SIMPLE_NETWORK_PROTOCOL_REVISION;
}

///
/// The EFI_SIMPLE_NETWORK_PROTOCOL protocol is used to initialize access
/// to a network adapter. Once the network adapter initializes,
/// the EFI_SIMPLE_NETWORK_PROTOCOL protocol provides services that
/// allow packets to be transmitted and received.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct EFI_SIMPLE_NETWORK_PROTOCOL
{
  ///
  /// Revision of the EFI_SIMPLE_NETWORK_PROTOCOL. All future revisions must
  /// be backwards compatible. If a future version is not backwards compatible
  /// it is not the same GUID.
  ///
  public ulong Revision;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_START*/ Start;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_STOP*/ Stop;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/,/* IN */ulong /*ExtraRxBufferSize*/,/* IN */ulong /*ExtraTxBufferSize*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_INITIALIZE*/ Initialize;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/,/* IN */bool /*ExtendedVerification*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_RESET*/ Reset;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_SHUTDOWN*/ Shutdown;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/,/* IN */uint /*Enable*/,/* IN */uint /*Disable*/,/* IN */bool /*ResetMCastFilter*/,/* IN */ulong /*MCastFilterCnt*/,/* IN */EFI_MAC_ADDRESS* /*MCastFilter*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_RECEIVE_FILTERS*/ ReceiveFilters;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/,/* IN */bool /*Reset*/,/* IN */EFI_MAC_ADDRESS* /*New*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_STATION_ADDRESS*/ StationAddress;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/,/* IN */bool /*Reset*/,/* IN OUT */ulong* /*StatisticsSize*/,/* OUT */EFI_NETWORK_STATISTICS* /*StatisticsTable*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_STATISTICS*/ Statistics;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/,/* IN */bool /*IPv6*/,/* IN */EFI_IP_ADDRESS* /*IP*/,/* OUT */EFI_MAC_ADDRESS* /*MAC*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_MCAST_IP_TO_MAC*/ MCastIpToMac;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/,/* IN */bool /*ReadWrite*/,/* IN */ulong /*Offset*/,/* IN */ulong /*BufferSize*/,/* IN OUT */void* /*Buffer*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_NVDATA*/ NvData;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/,/* OUT */uint* /*InterruptStatus*/,/* OUT */void** /*TxBuf*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_GET_STATUS*/ GetStatus;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/,/* IN */ulong /*HeaderSize*/,/* IN */ulong /*BufferSize*/,/* IN */void* /*Buffer*/,/* IN */EFI_MAC_ADDRESS* /*SrcAddr*/,/* IN */EFI_MAC_ADDRESS* /*DestAddr*/,/* IN */ushort* /*Protocol*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_TRANSMIT*/ Transmit;
  public readonly delegate* unmanaged</* IN */EFI_SIMPLE_NETWORK_PROTOCOL* /*This*/,/* OUT */ulong* /*HeaderSize*/,/* IN OUT */ulong* /*BufferSize*/,/* OUT */void* /*Buffer*/,/* OUT */EFI_MAC_ADDRESS* /*SrcAddr*/,/* OUT */EFI_MAC_ADDRESS* /*DestAddr*/,/* OUT */ushort* /*Protocol*/, EFI_STATUS> /*EFI_SIMPLE_NETWORK_RECEIVE*/ Receive;
  ///
  /// Event used with WaitForEvent() to wait for a packet to be received.
  ///
  public EFI_EVENT WaitForPacket;
  ///
  /// Pointer to the EFI_SIMPLE_NETWORK_MODE data for the device.
  ///
  public EFI_SIMPLE_NETWORK_MODE* Mode;
}

// extern EFI_GUID  gEfiSimpleNetworkProtocolGuid;

// #endif