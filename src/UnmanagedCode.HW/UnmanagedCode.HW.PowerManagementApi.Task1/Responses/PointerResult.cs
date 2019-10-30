using System;

namespace UnmanagedCode.HW.PowerManagementApi.Task1.Responses
{
    public class PointerResult
    {
        private const uint StatusSuccess = 0;

        public uint InteropResult { get; }

        public PointerResult(uint interopResult)
        {
            InteropResult = interopResult;
        }

        public bool IsSuccessful => InteropResult == StatusSuccess;
    }
}