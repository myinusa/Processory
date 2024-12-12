using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Logging;
using Processory.Native;
using Processory.Utilities;

namespace Processory.Internal {
    /// <summary>
    /// Provides functionality to read memory from a process.
    /// </summary>
    public class MemoryReader {
        private readonly ProcessoryClient processoryClient;
        private readonly ILogger logger;

        public MemoryReader(ProcessoryClient processoryClient, ILoggerFactory loggerFactory) {
            logger = loggerFactory.CreateLogger<MemoryReader>();
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
            return Encoding.ASCII.GetString(buffer, 0, length);
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
        public void ReadR(UIntPtr address, Span<byte> buffer) {
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

        public string ResolveStringPointerE(UIntPtr initialAddress, List<int> offsets) {
            UIntPtr address = processoryClient.PointerChainFollower.FollowPointerChain(initialAddress, offsets);
            var lengthOffset = new List<int>(offsets);
            lengthOffset[lengthOffset.Count - 1] = 0x0;
            var length = Read<int>(initialAddress, lengthOffset);

            Span<byte> buffer = stackalloc byte[length];
            ReadR(address, buffer);
            return Encoding.UTF8.GetString(buffer);
        }

        public List<string> ResolveStringPointerList(nuint initialAddress, List<int> offsets) {
            nuint address = processoryClient.PointerChainFollower.FollowPointerChain(initialAddress, offsets);
            List<int> list = new List<int>(offsets);
            list[list.Count - 1] = 0;
            Span<byte> span = stackalloc byte[Read<int>(initialAddress, list)];
            ReadR(address, span);

            // List to hold the found strings
            List<string> strings = new List<string>();
            Console.WriteLine(span.Length);

            // Iterate through the span to find segments separated by null terminators
            int startIndex = 0;
            while (startIndex < span.Length) {
                try {
                    // Find the index of the next null terminator (either 0 or 10)
                    int nullIndex = span.Slice(startIndex).IndexOfAny((byte)0, (byte)10);

                    // Console.WriteLine($"nullIndex: {nullIndex}");
                    // If no more null terminator is found, take the rest of the span
                    if (nullIndex == -1) {
                        // if (nullIndex < -1) {
                        nullIndex = span.Length - startIndex;
                        Span<byte> segment1 = span.Slice(startIndex, nullIndex);
                        strings.Add(Encoding.UTF8.GetString(segment1));
                        // Console.WriteLine($"segment: {Encoding.UTF8.GetString(segment1)}");
                        break;
                    }
                    else {
                        nullIndex += startIndex; // Adjust index to the original span
                    }

                    // Console.WriteLine($"startIndex: {startIndex}, span.Length: {span.Length}");
                    // Create a segment from startIndex to nullIndex
                    Span<byte> segment = span.Slice(startIndex, nullIndex - startIndex);
                    // Console.WriteLine($"segment: {Encoding.UTF8.GetString(segment)}");
                    strings.Add(Encoding.UTF8.GetString(segment));

                    // Move the startIndex past the null terminator
                    startIndex = nullIndex + 1; // +1 to skip the null terminator
                }
                catch (ArgumentOutOfRangeException ex) {
                    logger.LogError(ex, "An error occurred while resolving string pointer list.");
                    break; // Exit the loop if an error occurs
                }
            }

            return strings; // Return the list of strings
        }

        public T ReadUnsignedFileOffset<T>(UIntPtr address, string key)
            where T : unmanaged {
            Row foundRow = CSVDataOffsetManager.GetRowByStringName(key);
            UIntPtr addressChain = processoryClient.PointerChainFollower.FollowPointerChain(address.ToUInt64(), foundRow.Offsets);
            return processoryClient.MemoryReader.Read<T>(addressChain);
        }

        public T ReadAbsolute<T>(string key)
            where T : unmanaged {
            Row foundRow = CSVDataOffsetManager.GetRowByStringName(key);
            if (foundRow.Parent != string.Empty) {
                var parentAddress = processoryClient.AddressService.GetAbsoluteAddress(foundRow.Parent);
                logger.LogDebug("Parent address {Parent:X}", parentAddress);
                UIntPtr deferAddress = processoryClient.PointerChainFollower.DereferencePointer(parentAddress);

                UIntPtr resolvedAddress = processoryClient.PointerChainFollower.FollowPointerChain(deferAddress, foundRow.Offsets);
                logger.LogDebug("Resolved address {Resolved:X}", resolvedAddress);
                return processoryClient.MemoryReader.Read<T>(resolvedAddress);
            }

            var address = processoryClient.AddressService.GetAbsoluteAddress(key);
            return processoryClient.MemoryReader.Read<T>(address);
        }

        public string ReadOffsetString(UIntPtr address, string key) {
            return processoryClient.MemoryReader.ResolveStringPointerE(address, CSVDataOffsetManager.GetOffsetsByRowName(key));
        }
    }
}