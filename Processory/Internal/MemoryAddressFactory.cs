using Processory.Pointers;

namespace Processory.Internal;

/// <summary>
/// Factory class for creating memory address objects and managing memory addresses.
/// </summary>
public class MemoryAddressFactory(ProcessoryClient processoryClient) {
    private readonly RunTimeTypeInformation runTimeTypeInformation = new(processoryClient);
    private readonly MemoryPointer memoryPointer = new(processoryClient);
    private readonly MemoryReader memoryReader = new(processoryClient);

    public MemoryAddress<T> ReadMemoryLocation<T>(ulong memoryAddress)
    where T : unmanaged {
        return new MemoryAddress<T>(memoryAddress, memoryReader.Read<T>(memoryAddress));
    }

    private MemoryAddress<TPointer> GetPointerValue<TPointer>(ulong address)
        where TPointer : unmanaged {
        var pointerAddress = memoryPointer.Dereference(address);
        var pointerValue = memoryReader.Read<TPointer>(pointerAddress);
        return new MemoryAddress<TPointer>(pointerAddress, pointerValue);
    }

    public PointerInfo<TAddress, TPointer> Read<TAddress, TPointer>(ulong address)
        where TAddress : unmanaged
        where TPointer : unmanaged {
        var location = ReadMemoryLocation<TAddress>(address);
        var pointerInfo = GetPointerValue<TPointer>(address);

        return new PointerInfo<TAddress, TPointer>(location, pointerInfo);
    }

    public AddressManagerWithRTTI<TAddress, TPointer> ReadWithRTTI<TAddress, TPointer>(ulong address)
        where TAddress : unmanaged
        where TPointer : unmanaged {
        var location = ReadMemoryLocation<TAddress>(address);
        var pointerInfo = GetPointerValue<TPointer>(address);

        var rttiClassName = runTimeTypeInformation.GetRTTIClassName(address);

        return new AddressManagerWithRTTI<TAddress, TPointer>(location, pointerInfo, rttiClassName);
    }
}