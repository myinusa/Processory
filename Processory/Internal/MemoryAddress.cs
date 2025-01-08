namespace Processory.Internal;

public class MemoryAddress<T>(ulong address, T value) {
    public ulong Address { get; set; } = address;
    public T Value { get; set; } = value;
}

public class AddressManager<TAddress, TPointer> {
    public MemoryAddress<TAddress> Address { get; set; }
    public MemoryAddress<TPointer> Pointer { get; set; }

    public AddressManager(MemoryAddress<TAddress> address, MemoryAddress<TPointer> pointer) {
        Address = address;
        Pointer = pointer;
    }
}