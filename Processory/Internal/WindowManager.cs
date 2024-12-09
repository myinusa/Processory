using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Processory.Native;
using Processory.Native.User32;
using static Processory.Native.CursorManagement;
using static Processory.Native.Window.WindowInformation;
using static Processory.Native.Window.WindowManagement;
using static Processory.Native.Window.WindowState;
using static Processory.Native.WindowConstants;

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

    public void EnsureWindowIsForeground(IntPtr handle) {
        IntPtr foregroundWindow = GetForegroundWindow();
        if (handle != foregroundWindow) {
            logger.LogDebug("The provided window handle is not the foreground window.");
        }
        else {
            logger.LogWarning("The provided window handle is already the foreground window.");
        }
    }


    public void RestoreWindow(IntPtr handle) {
        if (IsIconic(handle)) {
            logger.LogDebug("Window is minimized, attempting to restore.");
            if (ShowWindow(handle, (int)ShowCommands.SW_RESTORE)) {
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
        if (SetForegroundWindow(handle)) {
            logger.LogDebug("Window set to foreground successfully.");
        }
        else {
            logger.LogError("Failed to set the window to foreground.");
            // Environment.Exit(1);
        }
    }

    public void BringWindowToFront(IntPtr handle) {
        if (handle == IntPtr.Zero) {
            logger.LogError("Invalid window handle.");
            return;
        }

        // Restore the window if it is minimized
        RestoreWindow(handle);

        // Attempt to set the window to the foreground
        if (!SetForegroundWindow(handle)) {
            logger.LogWarning("Initial attempt to set the window to foreground failed. Trying alternative methods.");

            // Try to bring the window to the top
            if (!BringWindowToTop(handle)) {
                int errorCode = Marshal.GetLastWin32Error();
                logger.LogError("Failed to bring the window to the top. Error code: {ErrorCode}", errorCode);
            }

            // Try to set the window position
            if (!SetWindowPos(handle, HWND_TOP, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW)) {
                int errorCode = Marshal.GetLastWin32Error();
                logger.LogError("Failed to set the window position. Error code: {ErrorCode}", errorCode);
            }

            // Retry setting the window to the foreground
            if (!SetForegroundWindow(handle)) {
                int errorCode = Marshal.GetLastWin32Error();
                logger.LogError("Final attempt to set the window to foreground failed. Error code: {ErrorCode}", errorCode);
            }
            else {
                logger.LogDebug("Window set to foreground successfully on retry.");
            }
        }
        else {
            logger.LogDebug("Window set to foreground successfully.");
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

        if (!MoveWindow(handle, rightHalfLeft, monitorInfo.Value.rcMonitor.Top, rightHalfWidth,
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
        nint monitor = MonitorManagement.MonitorFromWindow(handle, 0);
        MonitorInfo mi = new() {
            CbSize = (uint)Marshal.SizeOf<MonitorInfo>()
        };

        if (!MonitorManagement.GetMonitorInfo(monitor, ref mi)) {
            logger.LogError("Failed to get monitor info.");
            return null;
        }

        return mi;
    }


    public (string windowTitle, string processName)? GetWindowTitleAndProcessNameUnderCursor() {
        var cursorPos = default(Point);
        if (!GetCursorPos(ref cursorPos)) {
            logger.LogError("Failed to get cursor position.");
            return null;
        }

        IntPtr windowHandle = WindowFromPoint(cursorPos);
        if (windowHandle == IntPtr.Zero) {
            logger.LogError("No window found under cursor.");
            return null;
        }

        int length = GetWindowTextLength(windowHandle);
        if (length == 0) {
            logger.LogError("Failed to get window title length.");
            return null;
        }

        var windowTitle = new System.Text.StringBuilder(length + 1);
        GetWindowText(windowHandle, windowTitle, windowTitle.Capacity);

        uint processId;
        GetWindowThreadProcessId(windowHandle, out processId);

        var process = Process.GetProcessById((int)processId);
        string processName = process.ProcessName;

        return (windowTitle.ToString(), processName);
    }
}