namespace Processory.Internal;

public class MemoryAddress<T>(ulong address, T value) {
    public ulong Address { get; set; } = address;
    public T Value { get; set; } = value;
}

public class PointerInfo<TAddress, TPointer> {
    public MemoryAddress<TAddress> Address { get; set; }
    public MemoryAddress<TPointer> Pointer { get; set; }

    public PointerInfo(MemoryAddress<TAddress> address, MemoryAddress<TPointer> pointer) {
        Address = address;
        Pointer = pointer;
    }
}

public class AddressManagerWithRTTI<TAddress, TPointer> : PointerInfo<TAddress, TPointer> {
    public string RTTIClassName { get; set; }

    public AddressManagerWithRTTI(MemoryAddress<TAddress> address, MemoryAddress<TPointer> pointer, string rttiClassName)
        : base(address, pointer) {
        RTTIClassName = rttiClassName;
    }
}