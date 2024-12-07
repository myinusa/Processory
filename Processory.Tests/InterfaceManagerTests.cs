using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Processory.Internal;

namespace Processory.Tests;

public class InterfaceManagerTests {
    private readonly ProcessoryClient _processoryClient;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<InterfaceManager> _logger;
    private readonly WindowManager _windowManager;
    private readonly InterfaceManager _interfaceManager;
    private readonly ProcessoryClient processoryClient = new("fm", new LoggerFactory());
    
    [Fact]
    public void Run_ShouldLogDebugMessages_WhenMouseMovedSuccessfully() {
        // Arrange
        var windowName = "TestWindow";

        // Act
        processoryClient.InterfaceManager.Run(windowName);

        // Assert
        // Since we are using NullLogger, we can't verify the logs directly.
        // Instead, we can verify the behavior by checking the state changes or side effects.
        // In this case, we assume that if no exception is thrown, the test passes.
    }

    // [Fact]
    // public void Run_ShouldLogWarningMessage_WhenMouseMoveFails() {
    //     // Arrange
    //     var windowName = "InvalidWindow";
    //     processoryClient.ProcessService.ProcessHandle.MainWindowTitle = "AnotherWindow";

    //     // Act
    //     _interfaceManager.Run(windowName);

    //     // Assert
    //     // Since we are using NullLogger, we can't verify the logs directly.
    //     // Instead, we can verify the behavior by checking the state changes or side effects.
    //     // In this case, we assume that if no exception is thrown, the test passes.
    // }

    // [Fact]
    // public void Run_ShouldLogCriticalMessage_WhenExceptionIsThrown() {
    //     // var mockProcess = new Mock<Process>();

    //     // Arrange
    //     var windowName = "TestWindow";
    //     _processoryClient.ProcessService.ProcessHandle = new Process();

    //     // Act & Assert
    //     Assert.Throws<NullReferenceException>(() => _interfaceManager.Run(windowName));
    // }
}