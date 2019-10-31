using System.Runtime.InteropServices;

namespace UnmanagedCode.HW.PowerManagementApi.Task1.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ProcessorPowerInformation
    {
        /// <summary>
        /// The system processor number.
        /// </summary>
        public uint Number;
        /// <summary>
        /// The maximum specified clock frequency of the system processor, in megahertz.
        /// </summary>
        public uint MaxMhz;
        /// <summary>
        /// The processor clock frequency, in megahertz. This number is the maximum specified processor clock frequency multiplied by the current processor throttle.
        /// </summary>
        public uint CurrentMhz;
        /// <summary>
        /// The limit on the processor clock frequency, in megahertz. This number is the maximum specified processor clock frequency multiplied by the current processor thermal throttle limit.
        /// </summary>
        public uint MhzLimit;
        /// <summary>
        /// The maximum idle state of this processor.
        /// </summary>
        public uint MaxIdleState;
        /// <summary>
        /// The current idle state of this processor.
        /// </summary>
        public uint CurrentIdleState;

        public override string ToString()
        {
            return
$@"The system processor number: {Number}.
The maximum specified clock frequency of the system processor: {MaxMhz} Mhz.
The processor clock frequency: {CurrentMhz} Mhz.
The limit on the processor clock frequency: {MhzLimit} Mhz.
The maximum idle state of this processor: {MaxIdleState}.
The current idle state of this processor: {CurrentIdleState}.";
        }
    }
}
