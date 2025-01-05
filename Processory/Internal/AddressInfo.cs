namespace Processory.Internal;

public class AddressInfo<T>(ulong address, T value) {
    public ulong Address { get; set; } = address;
    public T Value { get; set; } = value;
}