using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Processory.Native;

namespace Processory.Internal;
public class RunTimeTypeInformation {
    private const int BaseOffsetPosition = 0x14;
    private const int ClassHierarchyDescriptorOffsetPosition = 0x10;
    private const int BaseClassCountPosition = 0x08;
    private const int BaseClassArrayOffsetPosition = 0x0C;
    private const int MaxBaseClassCount = 24;
    private readonly MemoryReader memoryReader;
    private readonly int objectLocatorOffset = nint.Size;

    public ProcessoryClient ProcessoryClient { get; set; }

    public RunTimeTypeInformation(ProcessoryClient processoryClient) {
        ProcessoryClient = processoryClient;
        memoryReader = ProcessoryClient.MemoryReader;
    }


    private nuint GetClassHierarchyDescriptorPtr(nuint objectLocatorPtr, nuint baseAddress, int offsetPosition) {
        int offset = memoryReader.Read<int>(objectLocatorPtr + (nuint)offsetPosition);
        return offset == 0 ? nuint.Zero : baseAddress + (nuint)offset;
    }

    private nuint GetBaseClassArrayPtr(nuint classHierarchyDescriptorPtr, nuint baseAddress, int offsetPosition) {
        int offset = memoryReader.Read<int>(classHierarchyDescriptorPtr + (nuint)offsetPosition);
        return offset == 0 ? nuint.Zero : baseAddress + (nuint)offset;
    }

    private string[]? GetBaseClassNames(nuint baseClassArrayPtr, nuint baseAddress, int baseClassCount) {
        if (baseClassCount < 0 || baseClassCount > int.MaxValue) {
            return null;
        }
        string[] names = new string[baseClassCount];
        for (int i = 0; i < baseClassCount; i++) {
            names[i] = GetBaseClassName(baseClassArrayPtr, baseAddress, i);
        }
        return names;
    }

    /// <summary>
    /// Gets a pointer to a memory location.
    /// </summary>
    /// <param name="arrayPtr">The base address of the array.</param>
    /// <param name="baseAddress">The base address to which the offset will be added.</param>
    /// <param name="offsetPosition">The position of the offset in the array.</param>
    /// <param name="index">The index of the element in the array.</param>
    /// <returns>Returns a pointer to the memory location. If the offset is 0, returns IntPtr.Zero.</returns>
    public nuint GetPointer(nuint arrayPtr, nuint baseAddress, int offsetPosition, int index) {
        int offset = memoryReader.Read<int>(arrayPtr + (nuint)(offsetPosition * index));
        return offset == 0 ? nuint.Zero : baseAddress + (nuint)offset;
    }

    /// <summary>
    /// Retrieves the name of the base class at the specified index.
    /// </summary>
    /// <param name="baseClassArrayPtr">The pointer to the base class array.</param>
    /// <param name="baseAddress">The base address of the object.</param>
    /// <param name="index">The index of the base class.</param>
    /// <returns>The name of the base class, or null if it cannot be retrieved.</returns>
    private string GetBaseClassName(nuint baseClassArrayPtr, nuint baseAddress, int index) {
        const int baseClassDescriptorOffsetPosition = 4;
        const int typeDescriptorOffsetPosition = 0;
        const int maxNameLength = 32;
        const int undecoratedOptions = 0x1000;
        const int typeDescriptorPosition = 0x14;

        nuint baseClassDescriptorPtr = GetPointer(baseClassArrayPtr, baseAddress, baseClassDescriptorOffsetPosition, index);
        if (baseClassDescriptorPtr == nuint.Zero) return string.Empty;

        nuint typeDescriptorPtr = GetPointer(baseClassDescriptorPtr, baseAddress, typeDescriptorOffsetPosition, 0);
        // IntPtr typeDescriptorPtr = IntPtr.Zero;
        if (typeDescriptorPtr == nuint.Zero) return string.Empty;

        string name = memoryReader.ReadString(typeDescriptorPtr + typeDescriptorPosition, maxNameLength);
        // string name = "";
        if (string.IsNullOrEmpty(name)) return string.Empty;

        // Log.Here().Information($"name: {name} | typeDescriptorPtr: {typeDescriptorPtr:X8} | baseClassDescriptorPtr: {baseClassDescriptorPtr:X8}");

        if (name.EndsWith("@@")) {
            var sb = new StringBuilder(255);
            NativeMethods.UnDecorateSymbolName("?" + name, sb, sb.Capacity, undecoratedOptions);
            name = sb.ToString();
        }

        int indexAtSymbol = name.IndexOf("@@");
        if (indexAtSymbol >= 0) {
            name = name.Substring(0, indexAtSymbol);
        }

        return name;
    }

    /// <summary>
    /// Retrieves the names of the RTTI (Run-Time Type Information) classes associated with the given address and process handle.
    /// </summary>
    /// <param name="address">The address of the RTTI structure.</param>
    /// <returns>An array of strings containing the names of the RTTI classes, or null if the retrieval fails.</returns>
    public string[] GetRTTIClass(ulong address) {
        // if (MemoryReader.ProcessHandler == nint.Zero) {
        //     throw new InvalidOperationException("Process handler is not set.");
        // }

        nuint structAddr = memoryReader.Read<nuint>(address);
        //if (!ProcessoryClient.AddressHelper.IsValidAddress((IntPtr)structAddr)) {
        //    return Array.Empty<string>();
        //}
        if (structAddr == nuint.Zero) return Array.Empty<string>();

        nuint objectLocatorPtr = memoryReader.Read<nuint>(structAddr - (nuint)objectLocatorOffset);
        //if (!ProcessoryClient.AddressHelper.IsValidAddress((IntPtr)objectLocatorPtr)) {
        //    return Array.Empty<string>();
        //}
        if (objectLocatorPtr == nuint.Zero) return Array.Empty<string>();

        var baseOffset = memoryReader.Read<int>((ulong)objectLocatorPtr + BaseOffsetPosition);
        //if (!ProcessoryClient.AddressHelper.IsValidAddress((IntPtr)objectLocatorPtr)) {
        //    return Array.Empty<string>();
        //}
        // if (baseOffset == IntPtr.Zero) return Array.Empty<string>();

        nuint baseAddress = nuint.Subtract(objectLocatorPtr, baseOffset);

        nuint classHierarchyDescriptorPtr = GetClassHierarchyDescriptorPtr(objectLocatorPtr, baseAddress, ClassHierarchyDescriptorOffsetPosition);
        //if (!ProcessoryClient.AddressHelper.IsValidAddress((IntPtr)classHierarchyDescriptorPtr)) {
        //    return Array.Empty<string>();
        //}
        if (classHierarchyDescriptorPtr == nuint.Zero) return Array.Empty<string>();

        int baseClassCount = memoryReader.Read<int>((ulong)classHierarchyDescriptorPtr + BaseClassCountPosition);
        //if (!ProcessoryClient.AddressHelper.IsValidAddress(baseClassCount)) {
        //    return Array.Empty<string>();
        //}
        if (baseClassCount == 0 || baseClassCount > MaxBaseClassCount) return Array.Empty<string>();

        nuint baseClassArrayPtr = GetBaseClassArrayPtr(classHierarchyDescriptorPtr, baseAddress, BaseClassArrayOffsetPosition);
        //if (!ProcessoryClient.AddressHelper.IsValidAddress((nint)baseClassArrayPtr)) {
        //    return Array.Empty<string>();
        //}
        if (baseClassArrayPtr == nuint.Zero) return Array.Empty<string>();

        return GetBaseClassNames(baseClassArrayPtr, baseAddress, baseClassCount) ?? Array.Empty<string>();
    }
}
