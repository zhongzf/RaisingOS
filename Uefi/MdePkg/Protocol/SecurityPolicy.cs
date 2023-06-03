namespace Uefi;
/** @file
  Security Policy protocol as defined in PI Specification VOLUME 2 DXE

  Copyright (c) 2006 - 2018, Intel Corporation. All rights reserved.<BR>
  SPDX-License-Identifier: BSD-2-Clause-Patent

**/

// #ifndef _SECURITY_POLICY_H_
// #define _SECURITY_POLICY_H_

public unsafe partial class EFI
{
  ///
  /// Security policy protocol GUID definition
  ///
  public static EFI_GUID EFI_SECURITY_POLICY_PROTOCOL_GUID = new GUID(0x78E4D245, 0xCD4D, 0x4a05, new byte[] { 0xA2, 0xBA, 0x47, 0x43, 0xE8, 0x6C, 0xFC, 0xAB });

  // extern EFI_GUID  gEfiSecurityPolicyProtocolGuid;
}

// #endif