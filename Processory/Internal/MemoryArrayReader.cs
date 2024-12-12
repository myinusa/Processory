using System.Runtime.InteropServices;

namespace Processory.Internal;

public class MemoryArrayReader(MemoryReader memoryReader) {
    private readonly MemoryReader memoryReader = memoryReader;

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
            if (!memoryReader.ReadProcessMemoryUnsafe((nuint)offset, ptr, (nuint)(array.Length * sizeof(T)))) {
                throw new InvalidOperationException($"Failed to read array at offset 0x{offset:X} with size {array.Length * sizeof(T)}.");
            }
        }
    }

    internal static T ByteArrayToStructure<T>(byte[] bytes)
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