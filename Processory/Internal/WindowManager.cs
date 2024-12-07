using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Processory.Native.User32;

namespace Processory.Internal;

public class WindowManager {
    private readonly ILogger logger;

    public WindowManager(ILogger logger) {
        this.logger = logger;
    }

    public bool IsValidWindow(IntPtr handle, string mainWindowTitle, string expectedTitle) {
        if (mainWindowTitle != expectedTitle) {
            logger.LogDebug("MainWindowTitle: {Title}", mainWindowTitle);
            return false;
        }

        if (handle == IntPtr.Zero) {
            logger.LogError("Invalid window handle.");
            return false;
        }

        return true;
    }

    public void RestoreWindow(IntPtr handle) {
        if (User32.IsIconic(handle)) {
            logger.LogDebug("Window is minimized, attempting to restore.");
            if (User32.ShowWindow(handle, (int)ShowCommands.SW_RESTORE)) {
                logger.LogDebug("Window restored successfully.");
            }
            else {
                int errorCode = Marshal.GetLastWin32Error();
                logger.LogWarning("Failed to restore the window. Error code: {ErrorCode}", errorCode);
            }
        }
        else {
            logger.LogDebug("Window is not minimized.");
        }
    }

    public void SetWindowToForeground(IntPtr handle) {
        if (User32.SetForegroundWindow(handle)) {
            logger.LogDebug("Window set to foreground successfully.");
        }
        else {
            Environment.Exit(1);
            logger.LogError("Failed to set the window to foreground.");
        }
    }

    public void SnapWindowToRightHalf(IntPtr handle) {
        MonitorInfo? monitorInfo = GetMonitorInfo(handle);
        if (monitorInfo == null) {
            logger.LogError("Failed to get monitor info for snapping window.");
            return;
        }

        int monitorWidth = monitorInfo.Value.rcMonitor.Right - monitorInfo.Value.rcMonitor.Left;
        int monitorHeight = monitorInfo.Value.rcMonitor.Bottom - monitorInfo.Value.rcMonitor.Top;
        int rightHalfWidth = monitorWidth / 2;
        int rightHalfLeft = monitorInfo.Value.rcMonitor.Left + rightHalfWidth;

        if (!User32.MoveWindow(handle, rightHalfLeft, monitorInfo.Value.rcMonitor.Top, rightHalfWidth,
                monitorHeight, true)) {
            int errorCode = Marshal.GetLastWin32Error();
            logger.LogWarning(
                "Failed to snap window to the right half of the screen. Error code: {ErrorCode}",
                errorCode);
        }
        else {
            logger.LogDebug("Window snapped to the right half of the screen successfully.");
        }
    }

    public MonitorInfo? GetMonitorInfo(IntPtr handle) {
        nint monitor = User32.MonitorFromWindow(handle, 0);
        MonitorInfo mi = new MonitorInfo {
            CbSize = (uint)Marshal.SizeOf<MonitorInfo>()
        };

        if (!User32.GetMonitorInfo(monitor, ref mi)) {
            logger.LogError("Failed to get monitor info.");
            return null;
        }

        return mi;
    }
}