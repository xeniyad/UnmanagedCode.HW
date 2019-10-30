using System;
using System.Runtime.InteropServices;
using UnmanagedCode.HW.PowerManagementApi.Task1.Structures;

namespace UnmanagedCode.HW.PowerManagementApi.Task1.Wrappers
{
    public class MarshalProvider
    {
        public void WriteBytes(IntPtr pointer, HibernateFileAction fileActions)
        {
            TryDo(() => Marshal.WriteByte(ptr: pointer, val: (byte)fileActions));
        }

        public void ReleasePointer(IntPtr intPtr)
        {
            TryDo(() => Marshal.FreeHGlobal(hglobal: intPtr));
        }

        public IntPtr AllocateMemory(int size)
        {
            return TryDo(() => Marshal.AllocCoTaskMem(cb: size));
        }

        public int SizeOf<TType>()
        {
            return TryDo(() => Marshal.SizeOf<bool>());
        }

        public T ToProperties<T>(IntPtr pointer)
        {
            return TryDo(() => Marshal.PtrToStructure<T>(ptr: pointer));
        }

        private static void TryDo(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Cannot do marshal operation", exception);
            }
        }

        private static T TryDo<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Cannot do marshal operation", exception);
            }
        }
    }
}