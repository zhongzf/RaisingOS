using System.Runtime.InteropServices;

namespace Uefi;
/** @file
  EFI_DEVICE_PATH_FROM_TEXT_PROTOCOL as defined in UEFI 2.0.
  This protocol provides service to convert text to device paths and device nodes.

  Copyright (c) 2006 - 2018, Intel Corporation. All rights reserved.<BR>
  SPDX-License-Identifier: BSD-2-Clause-Patent

**/

// #ifndef __DEVICE_PATH_FROM_TEXT_PROTOCOL_H__
// #define __DEVICE_PATH_FROM_TEXT_PROTOCOL_H__

public unsafe partial class EFI
{
  ///
  /// Device Path From Text protocol
  ///
  public static EFI_GUID EFI_DEVICE_PATH_FROM_TEXT_PROTOCOL_GUID = new GUID(
      0x5c99a21, 0xc70f, 0x4ad2, new byte[] { 0x8a, 0x5f, 0x35, 0xdf, 0x33, 0x43, 0xf5, 0x1e });

  // /**
  //   Convert text to the binary representation of a device node.
  // 
  //   @param  TextDeviceNode TextDeviceNode points to the text representation of a device
  //                          node. Conversion starts with the first character and continues
  //                          until the first non-device node character.
  // 
  //   @retval a_pointer      Pointer to the EFI device node.
  //   @retval NULL           if TextDeviceNode is NULL or there was insufficient memory.
  // 
  // **/
  // typedef
  // EFI_DEVICE_PATH_PROTOCOL *
  // (EFIAPI *EFI_DEVICE_PATH_FROM_TEXT_NODE)(
  //   IN CONST char                 *TextDeviceNode
  //   );

  // /**
  //   Convert text to the binary representation of a device node.
  // 
  //   @param  TextDeviceNode TextDevicePath points to the text representation of a device
  //                          path. Conversion starts with the first character and continues
  //                          until the first non-device path character.
  // 
  //   @retval a_pointer      Pointer to the allocated device path.
  //   @retval NULL           if TextDeviceNode is NULL or there was insufficient memory.
  // 
  // **/
  // typedef
  // EFI_DEVICE_PATH_PROTOCOL *
  // (EFIAPI *EFI_DEVICE_PATH_FROM_TEXT_PATH)(
  //   IN CONST char                 *TextDevicePath
  //   );
}

///
/// This protocol converts text to device paths and device nodes.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct EFI_DEVICE_PATH_FROM_TEXT_PROTOCOL
{
  public readonly delegate* unmanaged</* IN CONST */char* /*TextDeviceNode*/, EFI_STATUS> /*EFI_DEVICE_PATH_FROM_TEXT_NODE*/ ConvertTextToDeviceNode;
  public readonly delegate* unmanaged</* IN CONST */char* /*TextDevicePath*/, EFI_STATUS> /*EFI_DEVICE_PATH_FROM_TEXT_PATH*/ ConvertTextToDevicePath;
}

// extern EFI_GUID  gEfiDevicePathFromTextProtocolGuid;

// #endif
