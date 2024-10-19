using Microsoft.Extensions.Logging;
using Processory.Internal;
using Xunit.Abstractions;

namespace Processory.Tests {
    public class MemoryReaderTests {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly ProcessoryClient processoryClient = new("fm", new LoggerFactory());

        public MemoryReaderTests(ITestOutputHelper testOutputHelper) {
            this.testOutputHelper = testOutputHelper;
        }

        private const ulong BaseAddress = 0x140000000;
        private const ulong GameExceptionHandlerOffset = 0x04d50e30;


        [Fact]
        public void TestProcessIsValid()
        {
            // Arrange
            const ulong validAddress = BaseAddress + GameExceptionHandlerOffset; // Use a known valid address

            // Act
            var isValid = processoryClient.AddressHelper.IsValidAddress((IntPtr)validAddress);

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

            testOutputHelper.WriteLine("Address (BaseAddress + Offset): {0:X}", address);
            
            // Act
            var actualValue = processoryClient.MemoryReader.Read<int>(address);

            // Assert
            Assert.NotEqual(expectedValue, actualValue);
        }

        // public void Dispose()
        // {
        //     // Clean up resources if necessary
        //     processoryClient?.Dispose();
        // }
    }
}