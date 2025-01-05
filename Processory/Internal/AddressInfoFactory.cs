namespace Processory.Internal;
public class AddressInfoFactory(ProcessoryClient processoryClient) {
    private readonly ProcessoryClient processoryClient = processoryClient;

    public AddressInfo<T> ReadAddressInfo<T>(ulong address)
        where T : unmanaged {
        var value = processoryClient.MemoryReader.Read<T>(address);
        return new AddressInfo<T>(address, value);
    }

    public AddressPointer<TAddressValue, TPointerValue> ReadAddressPointer<TAddressValue, TPointerValue>(ulong address)
        where TAddressValue : unmanaged
        where TPointerValue : unmanaged {
        var addressInfo = ReadAddressInfo<TAddressValue>(address);

        var pointerAddress = processoryClient.PointerChainFollower.DereferencePointer(address);
        var pointerValue = processoryClient.MemoryReader.Read<TPointerValue>(pointerAddress);

        var pointerInfo = new AddressInfo<TPointerValue>(pointerAddress, pointerValue);

        return new AddressPointer<TAddressValue, TPointerValue>(addressInfo, pointerInfo);
    }
}