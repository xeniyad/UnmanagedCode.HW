using System;
using UnmanagedCode.HW.PowerManagementApi.Task1.Responses;
using UnmanagedCode.HW.PowerManagementApi.Task1.Structures;
using UnmanagedCode.HW.PowerManagementApi.Task1.Wrappers;

namespace UnmanagedCode.HW.PowerManagementApi.Task1
{
    public class HibernateFileManager
    {
        private readonly MarshalProvider _marshal;
        private readonly PowerManagementInteropWrapper _powerManagementInterop;

        public HibernateFileManager(MarshalProvider marshal, PowerManagementInteropWrapper powerManagementInterop)
        {
            _marshal = marshal;
            _powerManagementInterop = powerManagementInterop;
        }

        public PointerResult ReserveFile()
        {
            return HibernateFileAction(Structures.HibernateFileAction.Reserve);
        }

        public PointerResult DeleteFile()
        {
            return HibernateFileAction(Structures.HibernateFileAction.Delete);
        }

        private PointerResult HibernateFileAction(HibernateFileAction fileAction)
        {
            int intSize = _marshal.SizeOf<bool>();
            IntPtr pointer = _marshal.AllocateMemory(intSize);
            _marshal.WriteBytes(pointer, fileAction);

            const int outputBufferSize = 0;
            PointerResult result = _powerManagementInterop.CallNtPowerInformation(
                informationLevel: PowerInformationLevel.SystemReserveHiberFile,
                inputBuffer: pointer,
                inputBufSize: intSize,
                outputBuffer: IntPtr.Zero,
                outputBufferSize: outputBufferSize);

            _marshal.ReleasePointer(pointer);

            return result;
        }
    }
}
