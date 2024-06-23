using System.Diagnostics;
using System.Runtime.CompilerServices;
using Processory.Native;

namespace Processory.Memory;

public class MemoryReader {
    protected readonly ProcessoryClient ProcessoryClient;

    public MemoryReader(ProcessoryClient processoryClient) {
        this.ProcessoryClient = processoryClient;
    }

    public byte[] Read(UIntPtr address, int size) {
        var buffer = new byte[size];
        var bytesRead = MethodsNative.ReadProcessMemory(ProcessoryClient.ProcessHandle, address, buffer, (UIntPtr)size, out UIntPtr _);

        if (!bytesRead) {
            return Array.Empty<byte>();
        }

        return buffer;
    }

    public T Read<T>(ulong offset)
        where T : unmanaged {
        T value = default;
        ReadRef(offset, ref value);
        return value;
    }

    private unsafe bool ReadProcessMemory(UIntPtr location, void* buffer, nuint numBytes) {
        return MethodsNative.ReadProcessMemory(ProcessoryClient.ProcessHandle, location, (UIntPtr)buffer, numBytes, out _);
    }

    public unsafe void ReadRef<T>(ulong offset, ref T value)
        where T : unmanaged {
        void* buffer = Unsafe.AsPointer(ref value);
        if (!ReadProcessMemory((UIntPtr)offset, buffer, (nuint)sizeof(T))) {
            // ThrowHelpers.ThrowReadExternalMemoryExceptionWindows(offset, sizeof(T));
        }
    }

}