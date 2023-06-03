using System.Runtime.InteropServices;

namespace Uefi;
/** @file
  Device IO protocol as defined in the EFI 1.10 specification.

  Device IO is used to abstract hardware access to devices. It includes
  memory mapped IO, IO, PCI Config space, and DMA.

  Copyright (c) 2006 - 2018, Intel Corporation. All rights reserved.<BR>
  SPDX-License-Identifier: BSD-2-Clause-Patent

**/

// #ifndef __DEVICE_IO_H__
// #define __DEVICE_IO_H__

public unsafe partial class EFI
{
  public static EFI_GUID EFI_DEVICE_IO_PROTOCOL_GUID = new GUID(
      0xaf6ac311, 0x84c3, 0x11d2, new byte[] { 0x8e, 0x3c, 0x00, 0xa0, 0xc9, 0x69, 0x72, 0x3b });

  // typedef struct _EFI_DEVICE_IO_PROTOCOL EFI_DEVICE_IO_PROTOCOL;

  ///
  /// Protocol GUID name defined in EFI1.1.
  ///
  public static EFI_GUID DEVICE_IO_PROTOCOL = EFI_DEVICE_IO_PROTOCOL_GUID;
}

///
/// Protocol defined in EFI1.1.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct EFI_DEVICE_IO_INTERFACE { EFI_DEVICE_IO_PROTOCOL Value; public static implicit operator EFI_DEVICE_IO_INTERFACE(EFI_DEVICE_IO_PROTOCOL value) => new EFI_DEVICE_IO_INTERFACE() { Value = value }; public static implicit operator EFI_DEVICE_IO_PROTOCOL(EFI_DEVICE_IO_INTERFACE value) => value.Value; }

///
/// Device IO Access Width
///
public enum EFI_IO_WIDTH
{
  IO_UINT8 = 0,
  IO_UINT16 = 1,
  IO_UINT32 = 2,
  IO_UINT64 = 3,
  //
  // Below enumerations are added in "Extensible Firmware Interface Specification,
  // Version 1.10, Specification Update, Version 001".
  //
  MMIO_COPY_UINT8 = 4,
  MMIO_COPY_UINT16 = 5,
  MMIO_COPY_UINT32 = 6,
  MMIO_COPY_UINT64 = 7
}

// /**
//   Enables a driver to access device registers in the appropriate memory or I/O space.
// 
//   @param  This                  A pointer to the EFI_DEVICE_IO_INTERFACE instance.
//   @param  Width                 Signifies the width of the I/O operations.
//   @param  Address               The base address of the I/O operations.
//   @param  Count                 The number of I/O operations to perform.
//   @param  Buffer                For read operations, the destination buffer to store the results. For write
//                                 operations, the source buffer to write data from. If
//                                 Width is MMIO_COPY_UINT8, MMIO_COPY_UINT16,
//                                 MMIO_COPY_UINT32, or MMIO_COPY_UINT64, then
//                                 Buffer is interpreted as a base address of an I/O operation such as Address.
// 
//   @retval EFI_SUCCESS           The data was read from or written to the device.
//   @retval EFI_OUT_OF_RESOURCES  The request could not be completed due to a lack of resources.
//   @retval EFI_INVALID_PARAMETER Width is invalid.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_DEVICE_IO)(
//   IN EFI_DEVICE_IO_PROTOCOL         *This,
//   IN EFI_IO_WIDTH                   Width,
//   IN ulong                         Address,
//   IN ulong                          Count,
//   IN OUT void                       *Buffer
//   );

[StructLayout(LayoutKind.Sequential)]
public unsafe struct EFI_IO_ACCESS
{
  public readonly delegate* unmanaged</* IN */EFI_DEVICE_IO_PROTOCOL* /*This*/,/* IN */EFI_IO_WIDTH /*Width*/,/* IN */ulong /*Address*/,/* IN */ulong /*Count*/,/* IN OUT */void* /*Buffer*/, EFI_STATUS> /*EFI_DEVICE_IO*/ Read;
  public readonly delegate* unmanaged</* IN */EFI_DEVICE_IO_PROTOCOL* /*This*/,/* IN */EFI_IO_WIDTH /*Width*/,/* IN */ulong /*Address*/,/* IN */ulong /*Count*/,/* IN OUT */void* /*Buffer*/, EFI_STATUS> /*EFI_DEVICE_IO*/ Write;
}

// /**
//   Provides an EFI Device Path for a PCI device with the given PCI configuration space address.
// 
//   @param  This                  A pointer to the EFI_DEVICE_IO_INTERFACE instance.
//   @param  PciAddress            The PCI configuration space address of the device whose Device Path
//                                 is going to be returned.
//   @param  PciDevicePath         A pointer to the pointer for the EFI Device Path for PciAddress.
//                                 Memory for the Device Path is allocated from the pool.
// 
//   @retval EFI_SUCCESS           The PciDevicePath returns a pointer to a valid EFI Device Path.
//   @retval EFI_OUT_OF_RESOURCES  The request could not be completed due to a lack of resources.
//   @retval EFI_UNSUPPORTED       The PciAddress does not map to a valid EFI Device Path.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_PCI_DEVICE_PATH)(
//   IN EFI_DEVICE_IO_PROTOCOL           *This,
//   IN ulong                           PciAddress,
//   IN OUT EFI_DEVICE_PATH_PROTOCOL     **PciDevicePath
//   );

public enum EFI_IO_OPERATION_TYPE
{
  ///
  /// A read operation from system memory by a bus master.
  ///
  EfiBusMasterRead,

  ///
  /// A write operation to system memory by a bus master.
  ///
  EfiBusMasterWrite,

  ///
  /// Provides both read and write access to system memory
  /// by both the processor and a bus master. The buffer is
  /// coherent from both the processor's and the bus master's
  /// point of view.
  ///
  EfiBusMasterCommonBuffer
}

// /**
//   Provides the device-specific addresses needed to access system memory.
// 
//   @param  This                  A pointer to the EFI_DEVICE_IO_INTERFACE instance.
//   @param  Operation             Indicates if the bus master is going to read or write to system memory.
//   @param  HostAddress           The system memory address to map to the device.
//   @param  NumberOfBytes         On input, the number of bytes to map.
//                                 On output, the number of bytes that were mapped.
//   @param  DeviceAddress         The resulting map address for the bus master device to use to access the
//                                 hosts HostAddress.
//   @param  Mapping               A resulting value to pass to Unmap().
// 
//   @retval EFI_SUCCESS           The range was mapped for the returned NumberOfBytes.
//   @retval EFI_OUT_OF_RESOURCES  The request could not be completed due to a lack of resources.
//   @retval EFI_UNSUPPORTED       The HostAddress cannot be mapped as a common buffer.
//   @retval EFI_INVALID_PARAMETER The Operation or HostAddress is undefined.
//   @retval EFI_DEVICE_ERROR      The system hardware could not map the requested address.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_IO_MAP)(
//   IN EFI_DEVICE_IO_PROTOCOL           *This,
//   IN EFI_IO_OPERATION_TYPE            Operation,
//   IN EFI_PHYSICAL_ADDRESS             *HostAddress,
//   IN OUT ulong                        *NumberOfBytes,
//   OUT EFI_PHYSICAL_ADDRESS            *DeviceAddress,
//   OUT void                            **Mapping
//   );

// /**
//   Completes the Map() operation and releases any corresponding resources.
// 
//   @param  This                  A pointer to the EFI_DEVICE_IO_INTERFACE instance.
//   @param  Mapping               A resulting value to pass to Unmap().
// 
//   @retval EFI_SUCCESS           The range was mapped for the returned NumberOfBytes.
//   @retval EFI_DEVICE_ERROR      The system hardware could not map the requested address.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_IO_UNMAP)(
//   IN EFI_DEVICE_IO_PROTOCOL           *This,
//   IN void                             *Mapping
//   );

// /**
//   Allocates pages that are suitable for an EFIBusMasterCommonBuffer mapping.
// 
//   @param  This                  A pointer to the EFI_DEVICE_IO_INTERFACE instance.
//   @param  Type                  The type allocation to perform.
//   @param  MemoryType            The type of memory to allocate, EfiBootServicesData or
//                                 EfiRuntimeServicesData.
//   @param  Pages                 The number of pages to allocate.
//   @param  HostAddress           A pointer to store the base address of the allocated range.
// 
//   @retval EFI_SUCCESS           The requested memory pages were allocated.
//   @retval EFI_OUT_OF_RESOURCES  The memory pages could not be allocated.
//   @retval EFI_INVALID_PARAMETER The requested memory type is invalid.
//   @retval EFI_UNSUPPORTED       The requested HostAddress is not supported on
//                                 this platform.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_IO_ALLOCATE_BUFFER)(
//   IN EFI_DEVICE_IO_PROTOCOL           *This,
//   IN EFI_ALLOCATE_TYPE                Type,
//   IN EFI_MEMORY_TYPE                  MemoryType,
//   IN ulong                            Pages,
//   IN OUT EFI_PHYSICAL_ADDRESS         *HostAddress
//   );

// /**
//   Flushes any posted write data to the device.
// 
//   @param  This                  A pointer to the EFI_DEVICE_IO_INTERFACE instance.
// 
//   @retval EFI_SUCCESS           The buffers were flushed.
//   @retval EFI_DEVICE_ERROR      The buffers were not flushed due to a hardware error.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_IO_FLUSH)(
//   IN EFI_DEVICE_IO_PROTOCOL  *This
//   );

// /**
//   Frees pages that were allocated with AllocateBuffer().
// 
//   @param  This                  A pointer to the EFI_DEVICE_IO_INTERFACE instance.
//   @param  Pages                 The number of pages to free.
//   @param  HostAddress           The base address of the range to free.
// 
//   @retval EFI_SUCCESS           The requested memory pages were allocated.
//   @retval EFI_NOT_FOUND         The requested memory pages were not allocated with
//                                 AllocateBuffer().
//   @retval EFI_INVALID_PARAMETER HostAddress is not page aligned or Pages is invalid.
// 
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_IO_FREE_BUFFER)(
//   IN EFI_DEVICE_IO_PROTOCOL           *This,
//   IN ulong                            Pages,
//   IN EFI_PHYSICAL_ADDRESS             HostAddress
//   );

///
/// This protocol provides the basic Memory, I/O, and PCI interfaces that
/// are used to abstract accesses to devices.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct EFI_DEVICE_IO_PROTOCOL
{
  ///
  /// Allows reads and writes to memory mapped I/O space.
  ///
  public EFI_IO_ACCESS Mem;
  ///
  /// Allows reads and writes to I/O space.
  ///
  public EFI_IO_ACCESS Io;
  ///
  /// Allows reads and writes to PCI configuration space.
  ///
  public EFI_IO_ACCESS Pci;
  public readonly delegate* unmanaged</* IN */EFI_DEVICE_IO_PROTOCOL* /*This*/,/* IN */EFI_IO_OPERATION_TYPE /*Operation*/,/* IN */EFI_PHYSICAL_ADDRESS* /*HostAddress*/,/* IN OUT */ulong* /*NumberOfBytes*/,/* OUT */EFI_PHYSICAL_ADDRESS* /*DeviceAddress*/,/* OUT */void** /*Mapping*/, EFI_STATUS> /*EFI_IO_MAP*/ Map;
  public readonly delegate* unmanaged</* IN */EFI_DEVICE_IO_PROTOCOL* /*This*/,/* IN */ulong /*PciAddress*/,/* IN OUT */EFI_DEVICE_PATH_PROTOCOL** /*PciDevicePath*/, EFI_STATUS> /*EFI_PCI_DEVICE_PATH*/ PciDevicePath;
  public readonly delegate* unmanaged</* IN */EFI_DEVICE_IO_PROTOCOL* /*This*/,/* IN */void* /*Mapping*/, EFI_STATUS> /*EFI_IO_UNMAP*/ Unmap;
  public readonly delegate* unmanaged</* IN */EFI_DEVICE_IO_PROTOCOL* /*This*/,/* IN */EFI_ALLOCATE_TYPE /*Type*/,/* IN */EFI_MEMORY_TYPE /*MemoryType*/,/* IN */ulong /*Pages*/,/* IN OUT */EFI_PHYSICAL_ADDRESS* /*HostAddress*/, EFI_STATUS> /*EFI_IO_ALLOCATE_BUFFER*/ AllocateBuffer;
  public readonly delegate* unmanaged</* IN */EFI_DEVICE_IO_PROTOCOL* /*This*/, EFI_STATUS> /*EFI_IO_FLUSH*/ Flush;
  public readonly delegate* unmanaged</* IN */EFI_DEVICE_IO_PROTOCOL* /*This*/,/* IN */ulong /*Pages*/,/* IN */EFI_PHYSICAL_ADDRESS /*HostAddress*/, EFI_STATUS> /*EFI_IO_FREE_BUFFER*/ FreeBuffer;
}

// extern EFI_GUID  gEfiDeviceIoProtocolGuid;

// #endif