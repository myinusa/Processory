using Microsoft.Extensions.Logging;
using Processory.Native;
using Processory.Native.User32;
using static Processory.Native.Window.WindowInformation;
using static Processory.Native.Window.WindowManagement;
using static Processory.Native.Window.WindowState;

namespace Processory.Internal;
public class InterfaceManager {
    private readonly ProcessoryClient processoryClient;
    private readonly ILogger logger;
    private readonly WindowManager windowManager;
    private const int MillisecondsTimeout = 1000;

    public InterfaceManager(ProcessoryClient processoryClient, ILoggerFactory loggerFactory) {
        logger = loggerFactory.CreateLogger<InterfaceManager>();
        this.processoryClient = processoryClient;
        windowManager = new WindowManager(logger);
    }

    public void Run(string windowName) {
        try {
            logger.LogDebug("Starting WindowService");
            if (PrepareWindowAndMouse(windowName)) {
                logger.LogDebug("Mouse moved to center of monitor successfully.");
            }
            else {
                logger.LogWarning("Failed to move mouse to center of monitor.");
            }

            logger.LogDebug("Finished WindowService");
        }
        catch (Exception e) {
            logger.LogCritical(e, "An unhandled exception occurred while starting the application.");
        }
    }

    public bool PrepareWindowAndMouse(string windowTitle) {
        var handle = processoryClient.ProcessService.ProcessHandle!.MainWindowHandle;
        var mainWindowTitle = processoryClient.ProcessService.ProcessHandle.MainWindowTitle;

        if (!windowManager.IsValidWindow(handle, mainWindowTitle, windowTitle)) {
            logger.LogError("Invalid window handle.");
            return false;
        }

        var windowStatus = GetWindowStatus(handle);
        logger.LogDebug("Window status: {WindowStatus}", windowStatus);

        HandleWindowState(handle, windowStatus);

        if (!BringWindowToForeground(handle)) {
            return false;
        }
        windowManager.EnsureWindowIsForeground(handle);
        PerformWindowOperations(handle);

        var monitorInfo = windowManager.GetMonitorInfo(handle);
        if (monitorInfo == null) {
            return false;
        }

        MoveMouseToCenterOfRightHalf(monitorInfo.Value);
        return true;
    }

    private void HandleWindowState(IntPtr handle, string windowStatus) {
        if (windowStatus == "Minimized") {
            logger.LogDebug("Window is minimized, attempting to restore.");
            windowManager.RestoreWindow(handle);
            Thread.Sleep(MillisecondsTimeout);
        }

        if (windowStatus == "Restored") {
            logger.LogDebug("Window is restored, attempting to minimize and restore.");
            windowManager.MinimizeWindow(handle);
            Thread.Sleep(MillisecondsTimeout);
            windowManager.RestoreWindow(handle);
            Thread.Sleep(MillisecondsTimeout);
        }
    }

    private bool BringWindowToForeground(IntPtr handle) {
        int retryCount = 0;
        const int maxRetries = 3;

        while (retryCount < maxRetries) {
            logger.LogDebug("[1/4] Checking if the window is focused.");
            if (GetForegroundWindow() != handle) {
                logger.LogDebug("Window is not focused, attempting to bring it to the foreground.");
                TrySetForegroundWindow(handle, out _);
                logger.LogDebug("Window brought to the foreground.");
                Thread.Sleep(MillisecondsTimeout);
                retryCount++;
            }
            else {
                return true;
            }
        }

        logger.LogWarning("Failed to bring the window to the foreground after {MaxRetries} attempts.", maxRetries);
        return false;
    }

    private void PerformWindowOperations(IntPtr handle) {
        windowManager.RestoreWindow(handle);
        Thread.Sleep(MillisecondsTimeout);
        windowManager.BringWindowToFront(handle);
        Thread.Sleep(MillisecondsTimeout);
        windowManager.SnapWindowToRightHalf(handle);
        Thread.Sleep(MillisecondsTimeout);
    }

    private void MoveMouseToCenterOfRightHalf(MonitorInfo monitorInfo) {
        int monitorWidth = monitorInfo.RcMonitor.Right - monitorInfo.RcMonitor.Left;
        int rightHalfWidth = monitorWidth / 2;
        int rightHalfLeft = monitorInfo.RcMonitor.Left + rightHalfWidth;
        int centerX = rightHalfLeft + (rightHalfWidth / 2);
        int centerY = monitorInfo.RcMonitor.Top + ((monitorInfo.RcMonitor.Bottom - monitorInfo.RcMonitor.Top) / 2);

        CursorManagement.SetCursorPos(centerX, centerY);
        logger.LogDebug("Mouse moved to center of the right half of the screen successfully.");
    }

}