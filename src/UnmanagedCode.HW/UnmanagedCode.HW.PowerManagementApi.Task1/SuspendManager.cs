using System;
using System.ComponentModel;
using UnmanagedCode.HW.PowerManagementApi.Task1.Responses;
using UnmanagedCode.HW.PowerManagementApi.Task1.Wrappers;

namespace UnmanagedCode.HW.PowerManagementApi.Task1
{
    public class SuspendManager
    {
        private readonly PowerManagementInteropWrapper _powerManagementInterop;

        public SuspendManager(PowerManagementInteropWrapper powerManagementInterop)
        {
            _powerManagementInterop = powerManagementInterop
                ?? throw new ArgumentNullException(nameof(powerManagementInterop));
        }

        /// <summary>
        /// Suspends the system by shutting power down. Depending on the Hibernate parameter, the system either enters a suspend (sleep) state or hibernation (S4).
        /// </summary>
        /// <param name="hibernate">If this parameter is TRUE, the system hibernates. If the parameter is FALSE, the system is suspended.</param>
        /// <param name="forceCritical">This parameter has no effect. </param>
        /// <param name="disableWakeEvent">If this parameter is TRUE, the system disables all wake events. If the parameter is FALSE, any system wake events remain enabled.</param>
        public void SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent)
        {
            PointerResult result = _powerManagementInterop.SetSuspendState(hibernate, forceCritical, disableWakeEvent);

            // TODO Maxim: why does success result invoke an exception?
            if (result.IsSuccessful)
            {
                throw new Win32Exception();
            }
        }
    }
}
