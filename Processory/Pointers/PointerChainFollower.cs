namespace Processory.Pointers;

public class PointerChainFollower(ProcessoryClient processoryClient) {
    private readonly ProcessoryClient processoryClient = processoryClient;

    /// <summary>
    /// Reads a pointer value from the specified address and returns it as a <see cref="UIntPtr"/>.
    /// This method is useful for dereferencing an address that hasn't been read as a pointer yet.
    /// </summary>
    /// <param name="address">The address from which to read the pointer value.</param>
    /// <returns>A <see cref="UIntPtr"/> representing the pointer value read from the specified address.</returns>
    /// <example>
    /// Consider you have a base address and an offset, and you need to get the actual address that a pointer points to:
    /// <code language="csharp">
    /// ulong baseAddress = 0x12345678;
    /// ulong offset = 0x10;
    /// ulong combinedAddress = baseAddress + offset;
    ///
    /// UIntPtr pointerAddress = memoryReader.DereferencePointer(combinedAddress);
    /// </code>
    /// In this example, `DereferencePointer` reads the pointer value at the combined address (base address plus offset)
    /// and returns the actual address that the pointer points to.
    /// </example>
    public UIntPtr DereferencePointer(ulong address) {
        return new UIntPtr((uint)processoryClient.MemoryReader.ReadPointer((nint)address));
    }

    /// <summary>
    /// Follows a chain of pointers starting from a base address and applying a series of offsets.
    /// </summary>
    /// <param name="baseAddress">The starting address.</param>
    /// <param name="offsets">A list of offsets to apply to the base address.</param>
    /// <returns>The final address after applying all offsets.</returns>
    /// <example>
    /// For example, if the pointer chain is [[B95B19E0+D0]+B8]+4=>6034BEE0,
    /// the method starts reading from the base address B95B19E0,
    /// adds the offset D0, reads the value at that address,
    /// adds the offset B8, reads the value at that address,
    /// and finally adds the offset 4 to get the final address 6034BEE0.
    /// </example>
    public UIntPtr FollowPointerChain(ulong baseAddress, List<int> offsets) {
        if (offsets == null || offsets.Count == 0) {
            throw new ArgumentException("Offsets list cannot be null or empty.", nameof(offsets));
        }

        UIntPtr currentAddress = (UIntPtr)baseAddress;

        for (int i = 0; i < offsets.Count; ++i) {
            currentAddress = UIntPtr.Add(currentAddress, offsets[i]);
            if (i < offsets.Count - 1) {
                currentAddress = processoryClient.MemoryReader.ReadPointer<UIntPtr>((nint)currentAddress);
            }
        }

        return currentAddress;
    }

    public nuint ResolvePointerChain(ulong baseAddress, List<int> offsets) {
        if (offsets == null || offsets.Count == 0) {
            throw new ArgumentException("Offsets list cannot be null or empty.", nameof(offsets));
        }

        if (offsets.Count == 1) {
            offsets.Add(0x0);
        }

        nuint num = (nuint)baseAddress;
        for (int i = 0; i < offsets.Count; i++) {
            num = UIntPtr.Add(num, offsets[i]);
            if (i < offsets.Count - 1) {
                num = processoryClient.MemoryReader.ReadPointer<nuint>((nint)num);
            }
        }

        return num;
    }
}