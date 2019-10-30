namespace UnmanagedCode.HW.PowerManagementApi.Task1.Structures
{
    public enum PowerInformationLevel
    {
        /* This information level is not supported. */
        AdministratorPowerPolicy = 9,

        /*    The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER.
              The lpOutputBuffer buffer receives a ULONGLONG that specifies the interrupt-time count, 
              in 100-nanosecond units, at the last system sleep time.
        */
        LastSleepTime = 15,
        /* The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER. 
            The lpOutputBuffer buffer receives a ULONGLONG that specifies the interrupt-time count, 
            in 100-nanosecond units, at the last system wake time.  
          */
        LastWakeTime = 14,            
        ProcessorInformation = 11,
        ProcessorPowerPolicyAc = 18,
        ProcessorPowerPolicyCurrent = 22,
        ProcessorPowerPolicyDc = 19,
        /* The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER.
            The lpOutputBuffer buffer receives a SYSTEM_BATTERY_STATE structure containing information about the current system battery.
     */
        SystemBatteryState = 5,      
        SystemExecutionState  = 16,
        SystemPowerCapabilities = 4,
        /* The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER.
            The lpOutputBuffer buffer receives a SYSTEM_POWER_INFORMATION structure.
            Applications can use this level to retrieve information about the idleness of the system.
         */
        SystemPowerInformation = 12, 
        SystemPowerPolicyAc = 0,
        SystemPowerPolicyCurrent = 8,
        SystemPowerPolicyDc = 1,
        SystemReserveHiberFile = 10,
        VerifyProcessorPowerPolicyAc = 20,
        VerifyProcessorPowerPolicyDc = 21,
        VerifySystemPolicyAc = 2,
        VerifySystemPolicyDc = 3,

    }
}
