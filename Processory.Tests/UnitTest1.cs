using Xunit;
using System;
using System.Diagnostics;
using Processory;

namespace Processory.Tests;
public class ProcessoryTests {
    private Process? _testProcess;

    public ProcessoryTests() {
    }

    [Fact]
    public void Processory_Constructor_ShouldCreateInstanceWithValidHandle() {
        // Arrange
        string processName = "fm";

        // Act
        var processory = new ProcessoryClient(processName);

        // Assert
        Assert.NotEqual(IntPtr.Zero, processory.GetType().GetField("_processHandle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(processory));
    }

    [Fact]
    public void Processory_Constructor_ShouldThrowExceptionForNonExistentProcess() {
        // Arrange
        string nonExistentProcessName = "nonexistentprocess";

        // Act & Assert
        Assert.Throws<Exception>(() => new ProcessoryClient(nonExistentProcessName));
    }
}
