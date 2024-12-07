using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using Processory.Internal;

namespace Processory.Tests;
public class InterfaceMockTests {
    private readonly Mock<ProcessoryClient> _mockProcessoryClient;
    private readonly Mock<WindowManager> _mockWindowManager;
    private readonly InterfaceManager _interfaceManager;
    // private readonly ProcessoryClient processoryClient = new("fm", new LoggerFactory());

    public InterfaceMockTests() {
        Mock<ILoggerFactory> mockLoggerFactory = new();
        Mock<ILogger<InterfaceManager>> mockLogger = new();
        _mockWindowManager = new Mock<WindowManager>(mockLogger.Object);

        mockLoggerFactory.Setup(lf => lf.CreateLogger(It.IsAny<string>())).Returns(mockLogger.Object);
        _mockProcessoryClient = new Mock<ProcessoryClient>("fm", mockLoggerFactory.Object);

        _interfaceManager = new InterfaceManager(_mockProcessoryClient.Object, mockLoggerFactory.Object);
    }

    // [Fact]
    // public void Run_ShouldLogCriticalMessage_WhenExceptionIsThrown() {
    //     var mockProcess = new Mock<Process>();

    //     // Arrange
    //     var windowName = "TestWindow";
    //     _mockProcessoryClient.Setup(pc => pc.ProcessService.ProcessHandle).Returns(mockProcess.Object);

    //     // Act & Assert
    //     Assert.Throws<NullReferenceException>(() => _interfaceManager.Run(windowName));
    // }
}
