namespace Processory.Internal;
public class MemoryAddressFactory(ProcessoryClient processoryClient) {
    public MemoryAddress<T> ReadAddress<T>(ulong address)
        where T : unmanaged {
        var value = processoryClient.MemoryReader.Read<T>(address);
        return new MemoryAddress<T>(address, value);
    }

    public AddressManager<TAddress, TPointer> Read<TAddress, TPointer>(ulong address)
        where TAddress : unmanaged
        where TPointer : unmanaged {
        var addressInfo = ReadAddress<TAddress>(address);

        var pointerAddress = processoryClient.MemoryPointer.Dereference(address);
        var pointerValue = processoryClient.MemoryReader.Read<TPointer>(pointerAddress);

        var pointerInfo = new MemoryAddress<TPointer>(pointerAddress, pointerValue);

        return new AddressManager<TAddress, TPointer>(addressInfo, pointerInfo);
    }
}