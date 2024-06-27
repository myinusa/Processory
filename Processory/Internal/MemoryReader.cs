using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Processory.Native;
using Processory.Pointers;

namespace Processory.Internal {
    /// <summary>
    /// Provides functionality to read memory from a process.
    /// </summary>
    public class MemoryReader {
        private readonly ProcessoryClient processoryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryReader"/> class.
        /// </summary>
        /// <param name="processoryClient">The ProcessoryClient instance to use for memory operations.</param>
        public MemoryReader(ProcessoryClient processoryClient) {
            this.processoryClient = processoryClient ?? throw new ArgumentNullException(nameof(processoryClient));
        }

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

        public T Read<T>(ulong baseAddress, List<int> offsets)
            where T : unmanaged {
            nuint address = processoryClient.PointerChainFollower.FollowPointerChain(baseAddress, offsets);
            return Read<T>(address);
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
            if (!ReadProcessMemory((nuint)offset, buffer, (nuint)sizeof(T))) {
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
        private unsafe bool ReadProcessMemory(nuint location, void* buffer, nuint numBytes) {
            return MethodsNative.ReadProcessMemory(processoryClient.ProcessHandle, location, (nuint)buffer, numBytes, out _);
        }

        /// <summary>
        /// Reads an array of type T from the process memory at the specified offset.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="offset">The memory offset to read from.</param>
        /// <param name="count">The number of elements to read.</param>
        /// <returns>An array of type T read from the memory.</returns>
        public T[] ReadArray<T>(ulong offset, int count)
            where T : unmanaged {
            if (count <= 0) {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than zero.");
            }

            T[] array = new T[count];
            ReadArrayRef(offset, array);
            return array;
        }

        /// <summary>
        /// Reads an array of type T from the process memory at the specified offset into an existing array.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="offset">The memory offset to read from.</param>
        /// <param name="array">The array to store the read elements.</param>
        public unsafe void ReadArrayRef<T>(ulong offset, T[] array)
            where T : unmanaged {
            if (array == null) {
                throw new ArgumentNullException(nameof(array));
            }

            fixed (T* ptr = array) {
                if (!ReadProcessMemory((nuint)offset, ptr, (nuint)(array.Length * sizeof(T)))) {
                    throw new InvalidOperationException($"Failed to read array at offset 0x{offset:X} with size {array.Length * sizeof(T)}.");
                }
            }
        }

        /// <summary>
        /// Reads a null-terminated string from the process memory at the specified offset.
        /// </summary>
        /// <param name="offset">The memory offset to read from.</param>
        /// <param name="maxLength">The maximum length of the string to read.</param>
        /// <returns>The string read from the memory.</returns>
        public string ReadString(ulong offset, int maxLength = 1024) {
            if (maxLength <= 0) {
                throw new ArgumentOutOfRangeException(nameof(maxLength), "Max length must be greater than zero.");
            }

            byte[] buffer = Read((nuint)offset, maxLength);
            int nullTerminatorIndex = Array.IndexOf<byte>(buffer, 0);
            int length = nullTerminatorIndex >= 0 ? nullTerminatorIndex : buffer.Length;
            return System.Text.Encoding.ASCII.GetString(buffer, 0, length);
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

            return ByteArrayToStructure<T>(buffer);
        }


        private static T ByteArrayToStructure<T>(byte[] bytes)
            where T : struct {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T))!;
            }
            finally {
                handle.Free();
            }
        }
    }
}