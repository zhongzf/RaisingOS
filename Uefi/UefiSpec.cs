using System;
using System.Runtime.InteropServices;

namespace Uefi
{
  ///
  /// This provides the capabilities of the
  /// real time clock device as exposed through the EFI interfaces.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_TIME_CAPABILITIES
  {
    ///
    /// Provides the reporting resolution of the real-time clock device in
    /// counts per second. For a normal PC-AT CMOS RTC device, this
    /// value would be 1 Hz, or 1, to indicate that the device only reports
    /// the time to the resolution of 1 second.
    ///
    public uint Resolution;
    ///
    /// Provides the timekeeping accuracy of the real-time clock in an
    /// error rate of 1E-6 parts per million. For a clock with an accuracy
    /// of 50 parts per million, the value in this field would be
    /// 50,000,000.
    ///
    public uint Accuracy;
    ///
    /// A TRUE indicates that a time set operation clears the device's
    /// time below the Resolution reporting level. A FALSE
    /// indicates that the state below the Resolution level of the
    /// device is not cleared when the time is set. Normal PC-AT CMOS
    /// RTC devices set this value to FALSE.
    ///
    public bool SetsToZero;
  }

  ///
  /// EFI Runtime Services Table.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_RUNTIME_SERVICES
  {
    ///
    /// The table header for the EFI Runtime Services Table.
    ///
    EFI_TABLE_HEADER Hdr;

    //
    // Time Services
    //
    /**
      Returns the current time and date information, and the time-keeping capabilities
      of the hardware platform.

      @param[out]  Time             A pointer to storage to receive a snapshot of the current time.
      @param[out]  Capabilities     An optional pointer to a buffer to receive the real time clock
                                    device's capabilities.

      @retval EFI_SUCCESS           The operation completed successfully.
      @retval EFI_INVALID_PARAMETER Time is NULL.
      @retval EFI_DEVICE_ERROR      The time could not be retrieved due to hardware error.

    **/
    //EFI_GET_TIME GetTime;
    /**
      Sets the current local time and date information.

      @param[in]  Time              A pointer to the current time.

      @retval EFI_SUCCESS           The operation completed successfully.
      @retval EFI_INVALID_PARAMETER A time field is out of range.
      @retval EFI_DEVICE_ERROR      The time could not be set due due to hardware error.

    **/
    //EFI_SET_TIME SetTime;
    /**
     Returns the current wakeup alarm clock setting.

     @param[out]  Enabled          Indicates if the alarm is currently enabled or disabled.
     @param[out]  Pending          Indicates if the alarm signal is pending and requires acknowledgement.
     @param[out]  Time             The current alarm setting.

     @retval EFI_SUCCESS           The alarm settings were returned.
     @retval EFI_INVALID_PARAMETER Enabled is NULL.
     @retval EFI_INVALID_PARAMETER Pending is NULL.
     @retval EFI_INVALID_PARAMETER Time is NULL.
     @retval EFI_DEVICE_ERROR      The wakeup time could not be retrieved due to a hardware error.
     @retval EFI_UNSUPPORTED       A wakeup timer is not supported on this platform.

   **/
    //EFI_GET_WAKEUP_TIME GetWakeupTime;
    /**
      Sets the system wakeup alarm clock time.

      @param[in]  Enable            Enable or disable the wakeup alarm.
      @param[in]  Time              If Enable is TRUE, the time to set the wakeup alarm for.
                                    If Enable is FALSE, then this parameter is optional, and may be NULL.

      @retval EFI_SUCCESS           If Enable is TRUE, then the wakeup alarm was enabled. If
                                    Enable is FALSE, then the wakeup alarm was disabled.
      @retval EFI_INVALID_PARAMETER A time field is out of range.
      @retval EFI_DEVICE_ERROR      The wakeup time could not be set due to a hardware error.
      @retval EFI_UNSUPPORTED       A wakeup timer is not supported on this platform.

    **/
    //EFI_SET_WAKEUP_TIME SetWakeupTime;

    //
    // Virtual Memory Services
    //
    //EFI_SET_VIRTUAL_ADDRESS_MAP SetVirtualAddressMap;
    //EFI_CONVERT_POINTER ConvertPointer;

    //
    // Variable Services
    //
    //EFI_GET_VARIABLE GetVariable;
    //EFI_GET_NEXT_VARIABLE_NAME GetNextVariableName;
    //EFI_SET_VARIABLE SetVariable;

    //
    // Miscellaneous Services
    //
    //EFI_GET_NEXT_HIGH_MONO_COUNT GetNextHighMonotonicCount;
    //EFI_RESET_SYSTEM ResetSystem;

    //
    // UEFI 2.0 Capsule Services
    //
    //EFI_UPDATE_CAPSULE UpdateCapsule;
    //EFI_QUERY_CAPSULE_CAPABILITIES QueryCapsuleCapabilities;

    //
    // Miscellaneous UEFI 2.0 Service
    //
    //EFI_QUERY_VARIABLE_INFO QueryVariableInfo;
  }

  ///
  /// EFI Boot Services Table.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_BOOT_SERVICES
  {
    ///
    /// The table header for the EFI Boot Services Table.
    ///
    EFI_TABLE_HEADER Hdr;

    //
    // Task Priority Services
    //
    //EFI_RAISE_TPL RaiseTPL;
    //EFI_RESTORE_TPL RestoreTPL;

    //
    // Memory Services
    //
    //EFI_ALLOCATE_PAGES AllocatePages;
    //EFI_FREE_PAGES FreePages;
    //EFI_GET_MEMORY_MAP GetMemoryMap;
    //EFI_ALLOCATE_POOL AllocatePool;
    //EFI_FREE_POOL FreePool;

    //
    // Event & Timer Services
    //
    //EFI_CREATE_EVENT CreateEvent;
    //EFI_SET_TIMER SetTimer;
    //EFI_WAIT_FOR_EVENT WaitForEvent;
    //EFI_SIGNAL_EVENT SignalEvent;
    //EFI_CLOSE_EVENT CloseEvent;
    //EFI_CHECK_EVENT CheckEvent;

    //
    // Protocol Handler Services
    //
    //EFI_INSTALL_PROTOCOL_INTERFACE InstallProtocolInterface;
    //EFI_REINSTALL_PROTOCOL_INTERFACE ReinstallProtocolInterface;
    //EFI_UNINSTALL_PROTOCOL_INTERFACE UninstallProtocolInterface;
    //EFI_HANDLE_PROTOCOL HandleProtocol;
    //VOID* Reserved;
    //EFI_REGISTER_PROTOCOL_NOTIFY RegisterProtocolNotify;
    //EFI_LOCATE_HANDLE LocateHandle;
    //EFI_LOCATE_DEVICE_PATH LocateDevicePath;
    //EFI_INSTALL_CONFIGURATION_TABLE InstallConfigurationTable;

    //
    // Image Services
    //
    //EFI_IMAGE_LOAD LoadImage;
    //EFI_IMAGE_START StartImage;
    //EFI_EXIT Exit;
    //EFI_IMAGE_UNLOAD UnloadImage;
    //EFI_EXIT_BOOT_SERVICES ExitBootServices;

    //
    // Miscellaneous Services
    //
    //EFI_GET_NEXT_MONOTONIC_COUNT GetNextMonotonicCount;
    //EFI_STALL Stall;
    //EFI_SET_WATCHDOG_TIMER SetWatchdogTimer;

    //
    // DriverSupport Services
    //
    //EFI_CONNECT_CONTROLLER ConnectController;
    //EFI_DISCONNECT_CONTROLLER DisconnectController;

    //
    // Open and Close Protocol Services
    //
    //EFI_OPEN_PROTOCOL OpenProtocol;
    //EFI_CLOSE_PROTOCOL CloseProtocol;
    //EFI_OPEN_PROTOCOL_INFORMATION OpenProtocolInformation;

    //
    // Library Services
    //
    //EFI_PROTOCOLS_PER_HANDLE ProtocolsPerHandle;
    //EFI_LOCATE_HANDLE_BUFFER LocateHandleBuffer;
    //EFI_LOCATE_PROTOCOL LocateProtocol;
    //EFI_INSTALL_MULTIPLE_PROTOCOL_INTERFACES InstallMultipleProtocolInterfaces;
    //EFI_UNINSTALL_MULTIPLE_PROTOCOL_INTERFACES UninstallMultipleProtocolInterfaces;

    //
    // 32-bit CRC Services
    //
    //EFI_CALCULATE_CRC32 CalculateCrc32;

    //
    // Miscellaneous Services
    //
    //EFI_COPY_MEM CopyMem;
    //EFI_SET_MEM SetMem;
    //EFI_CREATE_EVENT_EX CreateEventEx;
  }


  ///
  /// Contains a set of GUID/pointer pairs comprised of the ConfigurationTable field in the
  /// EFI System Table.
  ///
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct EFI_CONFIGURATION_TABLE
  {
    ///
    /// The 128-bit GUID value that uniquely identifies the system configuration table.
    ///
    public EFI_GUID VendorGuid;
    ///
    /// A pointer to the table associated with VendorGuid.
    ///
    public void* VendorTable;
  }


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
    ///
    /// A pointer to the EFI Runtime Services Table.
    ///
    public EFI_RUNTIME_SERVICES* RuntimeServices;
    ///
    /// A pointer to the EFI Boot Services Table.
    ///
    public EFI_BOOT_SERVICES* BootServices;
    ///
    /// The number of system configuration tables in the buffer ConfigurationTable.
    ///
    public ulong NumberOfTableEntries;
    ///
    /// A pointer to the system configuration tables.
    /// The number of entries in the table is NumberOfTableEntries.
    ///
    public EFI_CONFIGURATION_TABLE* ConfigurationTable;
  }
}
