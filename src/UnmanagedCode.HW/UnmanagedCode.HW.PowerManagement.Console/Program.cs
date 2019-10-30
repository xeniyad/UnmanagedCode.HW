using System;
using UnmanagedCode.HW.PowerManagementApi.Task1;
using UnmanagedCode.HW.PowerManagementApi.Task1.Responses;
using UnmanagedCode.HW.PowerManagementApi.Task1.Wrappers;

namespace UnmanagedCode.HW.PowerManagement.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var powerManagementInterop = new PowerManagementInteropWrapper();
            var marshalProvider = new MarshalProvider();
            var powerManager = new PowerManager(marshalProvider, powerManagementInterop);

            DateTime lastSleepTime = powerManager.GetLastSleepTime();
            DateTime lastWakeTime = powerManager.GetLastWakeTime();
            var systemBatteryState = powerManager.GetSystemBatteryState();
            var powerInformation = powerManager.GetSystemPowerInformation();

            var hibernateFileManager = new HibernateFileManager(marshalProvider, powerManagementInterop);
            PointerResult result = hibernateFileManager.ReserveFile();

            if (!result.IsSuccessful)
            {
                throw new InvalidOperationException("Hibernate file reverse was unsuccessful.");
            }

            var suspendManager = new SuspendManager(powerManagementInterop);

            System.Console.WriteLine("Press any key to continue");
            System.Console.ReadKey();


        }

    }
}
