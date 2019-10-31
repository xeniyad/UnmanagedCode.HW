using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnmanagedCode.HW.PowerManagementApi.Task2COM;

namespace UnmanagedCode.HW.PowerManagementApi.Task3.Tests
{
    [TestClass]
    public class PowerManagerTests
    {
        private PowerManagerCustom _powerManagerCustom;

        [TestInitialize]
        public void Init()
        {
            _powerManagerCustom = new PowerManagerCustom();
        }

        [TestMethod]
        public void CheckSleepTimeTest()
        {
            var lastWakeTime = _powerManagerCustom.GetLastWakeTime();
            var lastSleepTime = _powerManagerCustom.GetLastSleepTime();

            bool sleep = lastSleepTime < lastWakeTime;
            bool rebooted = lastSleepTime == lastWakeTime;

            Assert.IsTrue(sleep || rebooted);
        }

        [TestMethod]
        public void TryGetBatteryStateTest()
        {
            var batteryState = _powerManagerCustom.GetSystemBatteryState();
            Assert.IsTrue(batteryState != null);
        }

        [TestMethod]
        public void TryGetProcessorInformation()
        {
            var processorPowerModel = _powerManagerCustom.GetProcessorPowerModel();
            Assert.IsTrue(
                processorPowerModel?.Items != null && 
                processorPowerModel.Items.Any()
                );
        }
    }
}
