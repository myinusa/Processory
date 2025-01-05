using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Processory.Tests; 
public class MemoryReaderTests(ITestOutputHelper testOutputHelper) : CommonTestBase(testOutputHelper) {
    private readonly ProcessoryClient processoryClient = new("fm", new LoggerFactory());
    private const ulong BaseAddress = 0x140000000;
    private const ulong GameExceptionHandlerOffset = 0x04d50e30;


    [Fact]
    public void TestProcessIsValid()
    {
        // Arrange
        const ulong validAddress = BaseAddress + GameExceptionHandlerOffset; // Use a known valid address

        // Act
        var isValid = processoryClient.AddressHelper.IsValidAddress((IntPtr)validAddress);

        // Log
        Logger.LogInformation("Address: {0:X}", validAddress);

        // Assert
        Assert.True(isValid, "The address should be valid.");
    }

    [Fact]
    public void TestReadMemory() {
        // Replace with a valid address and expected value
        // ulong address = 0xYourMemoryAddress; // Replace with a valid memory address

        const ulong addr1 = BaseAddress + GameExceptionHandlerOffset;
        var address = processoryClient.MemoryReader.Read<ulong>(addr1);

        const int expectedValue = 12345; // Replace with the expected value at that address

        // testOutputHelper.WriteLine("Address (BaseAddress + Offset): {0:X}", address);

        Logger.LogInformation("Address (BaseAddress + Offset): {0:X}", address);

        // Act
        var actualValue = processoryClient.MemoryReader.Read<int>(address);

        // Assert
        Assert.NotEqual(expectedValue, actualValue);
    }
}