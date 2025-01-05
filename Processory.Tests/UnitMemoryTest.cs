using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Processory.Tests;
public class MemoryReaderTests(ITestOutputHelper testOutputHelper) : CommonTestBase(testOutputHelper) {
    private readonly ProcessoryClient processoryClient = new("fm", new LoggerFactory());
    private const ulong BaseAddress = 0x140000000;
    private const ulong GameExceptionHandlerOffset = 0x04d50e30;


    [Fact]
    public void TestProcessIsValid() {
        const ulong validAddress = BaseAddress + GameExceptionHandlerOffset;
        var isValid = processoryClient.AddressHelper.IsValidAddress(validAddress);

        Logger.LogInformation("Address: {X}", validAddress);

        Assert.True(isValid, "The address should be valid.");
    }

    [Fact]
    public void TestReadMemory() {

        const ulong addr1 = BaseAddress + GameExceptionHandlerOffset;
        var address = processoryClient.MemoryReader.Read<ulong>(addr1);

        const int expectedValue = 12345;

        Logger.LogInformation("Address (BaseAddress + Offset): {X}", address);

        var actualValue = processoryClient.MemoryReader.Read<int>(address);

        Assert.NotEqual(expectedValue, actualValue);
    }
}