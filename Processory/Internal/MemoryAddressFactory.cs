namespace Processory.Internal;

/// <summary>
/// Factory class for creating memory address objects and managing memory addresses.
/// </summary>
public class MemoryAddressFactory(ProcessoryClient processoryClient) {
    /// <summary>
    /// Reads a value of type T from a specified memory address.
    /// </summary>
    /// <typeparam name="T">The type of the value to read. Must be an unmanaged type.</typeparam>
    /// <param name="address">The memory address to read from.</param>
    /// <returns>A MemoryAddress object containing the address and the read value.</returns>
    public MemoryAddress<T> Read<T>(ulong address)
        where T : unmanaged {
        var value = processoryClient.MemoryReader.Read<T>(address);
        return new MemoryAddress<T>(address, value);
    }

    /// <summary>
    /// Reads an address and its pointer value from a specified memory address.
    /// </summary>
    /// <typeparam name="TAddress">The type of the address to read. Must be an unmanaged type.</typeparam>
    /// <typeparam name="TPointer">The type of the pointer value to read. Must be an unmanaged type.</typeparam>
    /// <param name="address">The memory address to read from.</param>
    /// <returns>An AddressManager object containing both the address and pointer information.</returns>
    public AddressManager<TAddress, TPointer> Read<TAddress, TPointer>(ulong address)
        where TAddress : unmanaged
        where TPointer : unmanaged {
        var addressInfo = Read<TAddress>(address);

        var pointerAddress = processoryClient.MemoryPointer.Dereference(address);
        var pointerValue = processoryClient.MemoryReader.Read<TPointer>(pointerAddress);

        var pointerInfo = new MemoryAddress<TPointer>(pointerAddress, pointerValue);

        return new AddressManager<TAddress, TPointer>(addressInfo, pointerInfo);
    }
}