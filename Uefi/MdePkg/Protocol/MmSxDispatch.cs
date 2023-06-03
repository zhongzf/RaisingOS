using System.Runtime.InteropServices;

namespace Uefi;
/** @file
  MM Sx Dispatch Protocol as defined in PI 1.5 Specification
  Volume 4 Management Mode Core Interface.

  Provides the parent dispatch service for a given Sx-state source generator.

  Copyright (c) 2017, Intel Corporation. All rights reserved.<BR>
  SPDX-License-Identifier: BSD-2-Clause-Patent

**/

// #ifndef _MM_SX_DISPATCH_H_
// #define _MM_SX_DISPATCH_H_

// #include <Pi/PiMmCis.h>

public unsafe partial class EFI
{
  public static EFI_GUID EFI_MM_SX_DISPATCH_PROTOCOL_GUID = new GUID(
      0x456d2859, 0xa84b, 0x4e47, new byte[] { 0xa2, 0xee, 0x32, 0x76, 0xd8, 0x86, 0x99, 0x7d });
}

///
/// Sleep states S0-S5
///
public enum EFI_SLEEP_TYPE
{
  SxS0,
  SxS1,
  SxS2,
  SxS3,
  SxS4,
  SxS5,
  EfiMaximumSleepType
}

///
/// Sleep state phase: entry or exit
///
public enum EFI_SLEEP_PHASE
{
  SxEntry,
  SxExit,
  EfiMaximumPhase
}

///
/// The dispatch function's context
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct EFI_MM_SX_REGISTER_CONTEXT
{
  public EFI_SLEEP_TYPE Type;
  public EFI_SLEEP_PHASE Phase;
}

// typedef struct _EFI_MM_SX_DISPATCH_PROTOCOL EFI_MM_SX_DISPATCH_PROTOCOL;

// /**
//   Provides the parent dispatch service for a given Sx source generator.
// 
//   This service registers a function (DispatchFunction) which will be called when the sleep state
//   event specified by RegisterContext is detected. On return, DispatchHandle contains a
//   unique handle which may be used later to unregister the function using UnRegister().
//   The DispatchFunction will be called with Context set to the same value as was passed into
//   this function in RegisterContext and with CommBuffer and CommBufferSize set to
//   NULL and 0 respectively.
// 
//   @param[in] This                Pointer to the EFI_MM_SX_DISPATCH_PROTOCOL instance.
//   @param[in] DispatchFunction    Function to register for handler when the specified sleep state event occurs.
//   @param[in] RegisterContext     Pointer to the dispatch function's context.
//                                  The caller fills this context in before calling
//                                  the register function to indicate to the register
//                                  function which Sx state type and phase the caller
//                                  wishes to be called back on. For this intertace,
//                                  the Sx driver will call the registered handlers for
//                                  all Sx type and phases, so the Sx state handler(s)
//                                  must check the Type and Phase field of the Dispatch
//                                  context and act accordingly.
//   @param[out]  DispatchHandle    Handle of dispatch function, for when interfacing
//                                  with the parent Sx state MM driver.
// 
//   @retval EFI_SUCCESS            The dispatch function has been successfully
//                                  registered and the MMI source has been enabled.
//   @retval EFI_UNSUPPORTED        The Sx driver or hardware does not support that
//                                  Sx Type/Phase.
//   @retval EFI_DEVICE_ERROR       The Sx driver was unable to enable the MMI source.
//   @retval EFI_INVALID_PARAMETER  RegisterContext is invalid. Type & Phase are not
//                                  within valid range.
//   @retval EFI_OUT_OF_RESOURCES   There is not enough memory (system or MM) to manage this
//                                  child.
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_MM_SX_REGISTER)(
//   IN  CONST EFI_MM_SX_DISPATCH_PROTOCOL  *This,
//   IN        EFI_MM_HANDLER_ENTRY_POINT   DispatchFunction,
//   IN  CONST EFI_MM_SX_REGISTER_CONTEXT   *RegisterContext,
//   OUT       EFI_HANDLE                   *DispatchHandle
//   );

// /**
//   Unregisters an Sx-state service.
// 
//   This service removes the handler associated with DispatchHandle so that it will no longer be
//   called in response to sleep event.
// 
//   @param[in] This                Pointer to the EFI_MM_SX_DISPATCH_PROTOCOL instance.
//   @param[in] DispatchHandle      Handle of the service to remove.
// 
//   @retval EFI_SUCCESS            The service has been successfully removed.
//   @retval EFI_INVALID_PARAMETER  The DispatchHandle was not valid.
// **/
// typedef
// EFI_STATUS
// (EFIAPI *EFI_MM_SX_UNREGISTER)(
//   IN CONST EFI_MM_SX_DISPATCH_PROTOCOL  *This,
//   IN       EFI_HANDLE                   DispatchHandle
//   );

///
/// Interface structure for the MM Sx Dispatch Protocol
///
/// The EFI_MM_SX_DISPATCH_PROTOCOL provides the ability to install child handlers to
/// respond to sleep state related events.
///
[StructLayout(LayoutKind.Sequential)]
public unsafe struct EFI_MM_SX_DISPATCH_PROTOCOL
{
  public readonly delegate* unmanaged</* IN */CONST /*EFI_MM_SX_DISPATCH_PROTOCOL*/,/* IN */EFI_MM_HANDLER_ENTRY_POINT /*DispatchFunction*/,/* IN */CONST /*EFI_MM_SX_REGISTER_CONTEXT*/,/* OUT */EFI_HANDLE* /*DispatchHandle*/, EFI_STATUS> /*EFI_MM_SX_REGISTER*/ Register;
  public readonly delegate* unmanaged</* IN CONST */EFI_MM_SX_DISPATCH_PROTOCOL* /*This*/,/* IN */EFI_HANDLE /*DispatchHandle*/, EFI_STATUS> /*EFI_MM_SX_UNREGISTER*/ UnRegister;
}

// extern EFI_GUID  gEfiMmSxDispatchProtocolGuid;

// #endif