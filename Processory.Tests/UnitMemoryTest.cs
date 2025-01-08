using Microsoft.Extensions.Logging;
using Processory.Internal;
using Xunit.Abstractions;

namespace Processory.Tests;
// public class MemoryReaderTests(ITestOutputHelper testOutputHelper) : CommonTestBase(testOutputHelper) {
public class MemoryReaderTests(ITestOutputHelper testOutputHelper) {
    private readonly ProcessoryClient processoryClient = new("fm", new LoggerFactory());
    private const ulong BaseAddress = 0x140000000;

    private ulong ModuleBaseAddress => (ulong)processoryClient.ProcessService.GetModuleBaseAddress();

    private const ulong GameExceptionHandlerOffset = 0x04d50e30;
    protected Internal.MemoryReader MemoryReader => processoryClient.MemoryReader;


    // [Fact]
    // public void TestReadBytes() {
    //     const ulong address = 0x140000000;
    //     const int size = 4;
    //     byte[] result = MemoryReader.Read((nuint)address, size);

    //     Logger.LogInformation("Read bytes: {0}", BitConverter.ToString(result));

    //     Assert.NotEmpty(result);
    // }


    [Fact]
    public void TestProcessIsValid() {
        const ulong validAddress = BaseAddress + GameExceptionHandlerOffset;

        var isValid = processoryClient.AddressHelper.IsValidAddress(validAddress);

        // Logger.LogInformation("Address: {0:X}", validAddress);
        testOutputHelper.WriteLine("Address: {0:X}", validAddress);

        Assert.True(isValid, "The address should be valid.");
    }

    [Fact]
    public void TestReadMemory() {

        const ulong addr1 = BaseAddress + GameExceptionHandlerOffset;
        var address = processoryClient.MemoryReader.Read<ulong>(addr1);

        const int expectedValue = 12345;

        // Logger.LogInformation("Address (BaseAddress + Offset): {X}", address);

        var actualValue = processoryClient.MemoryReader.Read<int>(address);

        Assert.NotEqual(expectedValue, actualValue);
    }

    // [Fact]
    // [Trait("Category", "Pointer")]
    // public void ReadIntWithPointerChain() {
    //     var addr1 = ModuleBaseAddress + GameExceptionHandlerOffset;

    //     var offsets = new List<int> { 0x0, 0x0 };
    //     int result = MemoryReader.Read<int>(addr1, offsets);

    //     Logger.LogInformation("Read int with pointer chain: {0}", result);

    //     Assert.NotEqual(default, result);
    // }

    [Fact]
    [Trait("Category", "Pointer")]
    public void ResolvePointerChain() {
        var addr = ModuleBaseAddress + GameExceptionHandlerOffset;
        var addr1 = MemoryReader.ReadNo<nuint>(addr);

        var offsets = new List<int> { 0x0, 0x0 };
        var result = processoryClient.MemoryPointer.ResolvePointerChain(addr1, offsets);

        testOutputHelper.WriteLine("Resolved pointer chain result: {0}", result);

        Assert.NotEqual(default, result);
    }

    [Fact]
    [Trait("Category", "Pointer")]
    public void DeferencePointer() {
        var addr = ModuleBaseAddress + GameExceptionHandlerOffset;
        var addr1 = MemoryReader.ReadNo<nuint>(addr);

        // var offsets = new List<int> { 0x0 };
        var result = processoryClient.MemoryPointer.DereferencePointer(addr1);

        testOutputHelper.WriteLine("Read int with pointer chain: {0}", result);

        Assert.NotEqual(default, result);
    }

    [Fact]
    [Trait("Category", "Pointer")]
    public void ReadPointerCE() {
        var addr1 = ModuleBaseAddress + GameExceptionHandlerOffset;
        // var addr1 = MemoryReader.ReadNo<nuint>(addr);

        // var offsets = new List<int> { 0x0 };
        // var result = Internal.MemoryReader.ReadPointer(addr1);
        var result = MemoryReader.ReadPointerCE(addr1);

        testOutputHelper.WriteLine("Read int with pointer chain: {0}", result);

        Assert.NotEqual(default, result);
    }

    // [Fact]
    // [Trait("Category", "Address")]
    // public void ReadPointerInfoTest() {
    //     // Arrange
    //     var addr = ModuleBaseAddress + GameExceptionHandlerOffset;

    //     // Act
    //     var pointerInfo = MemoryReader.ReadPointerInfo(addr);

    //     // Assert
    //     testOutputHelper.WriteLine("Pointer Value: {0}", pointerInfo.PointerValue);
    //     testOutputHelper.WriteLine("Reference: {0}", pointerInfo.Reference);
    //     testOutputHelper.WriteLine("Address Being Pointed To: {0}", pointerInfo.AddressBeingPointedTo);

    //     Assert.NotEqual(default, pointerInfo.PointerValue);
    //     Assert.NotEqual(default, pointerInfo.Reference);
    //     Assert.NotEqual(default, pointerInfo.AddressBeingPointedTo);
    // }

    [Fact]
    [Trait("Category", "Address")]
    public void TestReadAddressInfo() {
        // Arrange
        const ulong address = BaseAddress + GameExceptionHandlerOffset;

        // Act
        // var addressInfo = MemoryReader.ReadAddressInfo<int>(address);
        var addressInfo = processoryClient.MemoryAddressFactory.ReadAddress<int>(address);


        // var addressInfoCE = MemoryReader.ReadPointerCE(address);
        var pointerAddress = processoryClient.MemoryPointer.DereferencePointer(address);
        var pointerValue = MemoryReader.Read<int>(pointerAddress);

        testOutputHelper.WriteLine("=======================");
        testOutputHelper.WriteLine("Address: {0:X8}", addressInfo.Address);
        testOutputHelper.WriteLine("Value: {0}", addressInfo.Value);
        testOutputHelper.WriteLine("=======================");

        testOutputHelper.WriteLine("P->Address: {0:X8}", pointerAddress);
        testOutputHelper.WriteLine("P->Value: {0}", pointerValue);
        testOutputHelper.WriteLine("=======================");

        Assert.Equal(address, addressInfo.Address);
        Assert.NotEqual(default, addressInfo.Value);
    }

    [Fact]
    [Trait("Category", "Address")]
    public void ReadAddressPointerInfo() {
        const ulong address = BaseAddress + GameExceptionHandlerOffset;

        // var addressPointerInfo = MemoryReader.ReadAddressPointer<int, int>(address);
        var addressPointerInfo = processoryClient.MemoryAddressFactory.Read<int, int>(address);


        testOutputHelper.WriteLine("=======================");
        testOutputHelper.WriteLine("Address: {0:X8}", addressPointerInfo.Address.Address);
        testOutputHelper.WriteLine("Value: {0}", addressPointerInfo.Address.Value);
        testOutputHelper.WriteLine("=======================");

        testOutputHelper.WriteLine("P->Address: {0:X8}", addressPointerInfo.Pointer.Address);
        testOutputHelper.WriteLine("P->Value: {0}", addressPointerInfo.Pointer.Value);
        testOutputHelper.WriteLine("=======================");

        Assert.Equal(address, addressPointerInfo.Address.Address);
        Assert.NotEqual(default, addressPointerInfo.Address.Value);
    }
}