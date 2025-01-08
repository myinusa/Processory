using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Processory.Native;
using Processory.Utilities;

namespace Processory.Internal {
    /// <summary>
    /// Provides functionality to read memory from a process.
    /// </summary>
    public class MemoryReader(ProcessoryClient processoryClient, ILoggerFactory loggerFactory) {
        private readonly ProcessoryClient processoryClient = processoryClient ?? throw new ArgumentNullException(nameof(processoryClient));
        private readonly ILogger logger = loggerFactory.CreateLogger<MemoryReader>();

        /// <summary>
        /// Reads a specified number of bytes from the process memory at the given address.
        /// </summary>
        /// <param name="address">The memory address to read from.</param>
        /// <param name="size">The number of bytes to read.</param>
        /// <returns>An array of bytes read from the memory, or an empty array if the read operation fails.</returns>
        public byte[] Read(nuint address, int size) {
            if (size <= 0) {
                throw new ArgumentOutOfRangeException(nameof(size), "Size must be greater than zero.");
            }

            var buffer = new byte[size];
            bool success = ReadProcessMemory(address, buffer, (nuint)size, out _);

            return success ? buffer : Array.Empty<byte>();
        }

        /// <summary>
        /// Reads a value of type T from the process memory at the specified offset.
        /// </summary>
        /// <typeparam name="T">The type of value to read.</typeparam>
        /// <param name="offset">The memory offset to read from.</param>
        /// <returns>The value read from the memory.</returns>
        public T Read<T>(ulong offset)
            where T : unmanaged {
            T value = default;
            ReadRef(offset, ref value);
            return value;
        }

        /// <summary>
        /// Reads a value of type T from the process memory at the specified offset after following a chain of pointers.
        /// </summary>
        /// <typeparam name="T">The type of value to read.</typeparam>
        /// <param name="baseAddress">The starting memory address.</param>
        /// <param name="offsets">The chain of offsets to follow.</param>
        /// <returns>The value read from the memory after following the chain of pointers.</returns>
        public T Read<T>(ulong baseAddress, List<int> offsets)
            where T : unmanaged {
            nuint address = processoryClient.MemoryPointer.FollowPointerChain(baseAddress, offsets);
            return Read<T>(address);
        }

        public T ReadNo<T>(ulong baseAddress)
            where T : unmanaged {
            // nuint address = processoryClient.MemoryPointer.FollowPointerChain(baseAddress, offsets);
            return Read<T>(baseAddress);
        }

        /// <summary>
        /// Reads a value of type T from the process memory at the specified offset into a reference.
        /// </summary>
        /// <typeparam name="T">The type of value to read.</typeparam>
        /// <param name="offset">The memory offset to read from.</param>
        /// <param name="value">A reference to store the read value.</param>
        public unsafe void ReadRef<T>(ulong offset, ref T value)
            where T : unmanaged {
            void* buffer = Unsafe.AsPointer(ref value);
            if (!ReadProcessMemoryUnsafe((nuint)offset, buffer, (nuint)sizeof(T))) {
                //throw new InvalidOperationException($"Failed to read memory at offset 0x{offset:X} with size {sizeof(T)}.");
            }
        }

        /// <summary>
        /// Reads process memory using native methods.
        /// </summary>
        /// <param name="baseAddress">The base address to read from.</param>
        /// <param name="buffer">The buffer to store the read data.</param>
        /// <param name="size">The number of bytes to read.</param>
        /// <param name="numberOfBytesRead">The number of bytes actually read.</param>
        /// <returns>True if the read operation succeeds, false otherwise.</returns>
        private bool ReadProcessMemory(nuint baseAddress, byte[] buffer, nuint size, out nuint numberOfBytesRead) {
            return MethodsNative.ReadProcessMemory(processoryClient.ProcessHandle, baseAddress, buffer, size, out numberOfBytesRead);
        }

        /// <summary>
        /// Reads process memory using native methods with unsafe pointers.
        /// </summary>
        /// <param name="location">The memory location to read from.</param>
        /// <param name="buffer">A pointer to the buffer to store the read data.</param>
        /// <param name="numBytes">The number of bytes to read.</param>
        /// <returns>True if the read operation succeeds, false otherwise.</returns>
        internal unsafe bool ReadProcessMemoryUnsafe(nuint location, void* buffer, nuint numBytes) {
            return MethodsNative.ReadProcessMemory(processoryClient.ProcessHandle, location, (nuint)buffer, numBytes, out _);
        }

        public MemoryAddress<T> ReadAddressInfo<T>(ulong address)
            where T : unmanaged {
            var value = Read<T>(address);
            return new MemoryAddress<T>(address, value);
        }

        public nuint ReadPointerCE(ulong address) {
            var addr1 = Read<nuint>(address);
            return processoryClient.MemoryPointer.DereferencePointer(addr1);
        }

        public nint ReadPointer(nint address) {
            byte[] buffer = Read((nuint)address, nint.Size);

            if (buffer.Length == 0) {
                return nint.Zero;
            }

            return Marshal.ReadIntPtr(Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0));
        }

        /// <summary>
        /// Reads a value of type T from the specified address.
        /// </summary>
        /// <typeparam name="T">The type of value to read.</typeparam>
        /// <param name="address">The address to read from.</param>
        /// <returns>The value read from the address.</returns>
        public T ReadPointer<T>(nint address)
            where T : struct {
            int size = Marshal.SizeOf<T>();
            byte[] buffer = Read((nuint)address, size);

            if (buffer.Length == 0) {
                return default;
            }

            return MemoryArrayReader.ByteArrayToStructure<T>(buffer);
        }

        /// <summary>
        /// Reads a specified number of bytes from the process memory at the given address into a Span buffer.
        /// </summary>
        /// <param name="address">The memory address to read from.</param>
        /// <param name="buffer">The Span buffer to store the read data.</param>
        public void ReadSpanBytes(UIntPtr address, Span<byte> buffer) {
            if (buffer.Length == 0) {
                return;
            }

            ref byte bufferRef = ref MemoryMarshal.GetReference(buffer);
            var bytesRead = MethodsNative.ReadProcessMemory(processoryClient.ProcessHandle, address, ref bufferRef, (UIntPtr)buffer.Length, out UIntPtr _);

            if (!bytesRead) {
                // Handle the error as needed, e.g., throw an exception or log the error.
                // throw new InvalidOperationException($"Failed to read memory at address 0x{address:X} with buffer length {buffer.Length}.");
            }
        }

        public T ReadUnsignedFileOffset<T>(UIntPtr address, string key)
            where T : unmanaged {
            Row foundRow = CSVDataOffsetManager.GetRowByStringName(key);
            UIntPtr addressChain = processoryClient.MemoryPointer.FollowPointerChain(address.ToUInt64(), foundRow.Offsets);
            return processoryClient.MemoryReader.Read<T>(addressChain);
        }
    }
}