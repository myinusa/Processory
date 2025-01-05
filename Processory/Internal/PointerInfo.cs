namespace Processory.Internal;
/// <summary>
/// Holds information about a pointer, including its value, reference, and the address being pointed to.
/// </summary>
public class PointerInfo(nuint pointerValue, nuint reference, nuint addressBeingPointedTo) {
    public nuint PointerValue { get; } = pointerValue;
    public nuint Reference { get; } = reference;
    public nuint AddressBeingPointedTo { get; } = addressBeingPointedTo;
}