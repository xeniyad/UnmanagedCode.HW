using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using UnmanagedCode.HW.PowerManagementApi.Task1.Responses;
using UnmanagedCode.HW.PowerManagementApi.Task1.Structures;
using UnmanagedCode.HW.PowerManagementApi.Task1.Wrappers;

namespace UnmanagedCode.HW.PowerManagementApi.Task1
{
    public class PowerManager
    {
        private readonly MarshalProvider _marshal;

        private readonly PowerManagementInteropWrapper _powerManagementInterop;

        public PowerManager(MarshalProvider marshal, PowerManagementInteropWrapper powerManagementInterop)
        {
            _marshal = marshal
                ?? throw new ArgumentNullException(nameof(marshal));
            _powerManagementInterop = powerManagementInterop
                                      ?? throw new ArgumentNullException(nameof(powerManagementInterop));
        }

        private DateTime GetLastBootUpTime()
        {
            var osClass = new ManagementClass("Win32_OperatingSystem");
            var properties = new List<PropertyData>();

            foreach (var queryObj in osClass.GetInstances())
            {
                properties.AddRange(queryObj.Properties.Cast<PropertyData>());
            }

            var lastBootUpProperty = properties.First(x => x.Name == "LastBootUpTime");
            DateTime dateTime = ManagementDateTimeConverter.ToDateTime(lastBootUpProperty.Value.ToString());
            return dateTime;
        }

        private T GetStructure<T>(PowerInformationLevel informationLevel)
        {
            IntPtr lpInBuffer = IntPtr.Zero;
            int inputBufSize = 0;
            int outputPtrSize = _marshal.SizeOf<T>();
            IntPtr outputPtr = _marshal.AllocateMemory(size: outputPtrSize);

            PointerResult result = _powerManagementInterop.CallNtPowerInformation(
                informationLevel: informationLevel,
                inputBuffer: lpInBuffer,
                inputBufSize: inputBufSize,
                outputBuffer: outputPtr,
                outputBufferSize: outputPtrSize);

            _marshal.ReleasePointer(lpInBuffer);

            if (result.IsSuccessful)
            {
                T properties = _marshal.ToProperties<T>(outputPtr);
                _marshal.ReleasePointer(outputPtr);
                return properties;
            }
            else
            {
                _marshal.ReleasePointer(outputPtr);
                throw new Win32Exception();
            }
        }

        public DateTime GetLastSleepTime()
        {
            long lastSleepTimeTicks = GetStructure<long>(PowerInformationLevel.LastSleepTime);

            DateTime bootUpTime = GetLastBootUpTime();
            DateTime lastSleepTime = bootUpTime.AddTicks(lastSleepTimeTicks);
            return lastSleepTime;
        }

        public DateTime GetLastWakeTime()
        {
            long lastWakeTimeTicks = GetStructure<long>(PowerInformationLevel.LastWakeTime);

            DateTime bootUpTime = GetLastBootUpTime();
            DateTime lastWakeTime = bootUpTime.AddTicks(lastWakeTimeTicks);
            return lastWakeTime;
        }


        public SystemBatteryState GetSystemBatteryState()
        {
            var batteryState = GetStructure<SystemBatteryState>(PowerInformationLevel.SystemBatteryState);

            return batteryState;

        }

        public ProcessorPowerInformation[] GetSystemPowerInformation()
        {
            var procCount = Environment.ProcessorCount;
            var procInfo = new ProcessorPowerInformation[procCount];

            var nOutputBufferSize = procInfo.Length * _marshal.SizeOf<ProcessorPowerInformation>();

            PointerResult result = _powerManagementInterop.CallNtPowerInformation(
                informationLevel:PowerInformationLevel.ProcessorInformation,
                lpInputBuffer: IntPtr.Zero,
                inputBufSize: 0,
                lpOutputBuffer: procInfo,
                nOutputBufferSize: nOutputBufferSize);

            if (!result.IsSuccessful)
            {
                throw new Win32Exception();
            }

            return procInfo;
        }

    }
}
