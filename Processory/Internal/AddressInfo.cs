namespace Processory.Internal;

public class AddressInfo<T>(ulong address, T value) {
    public ulong Address { get; set; } = address;
    public T Value { get; set; } = value;
}

public class AddressPointer<TAddressValue, TPointerValue>(AddressInfo<TAddressValue> addressInfo, AddressInfo<TPointerValue> pointerInfo) {
    public AddressInfo<TAddressValue> AddressInfo { get; set; } = addressInfo;
    public AddressInfo<TPointerValue> PointerInfo { get; set; } = pointerInfo;
}