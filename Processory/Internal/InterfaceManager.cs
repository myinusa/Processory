using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Processory.Native.User32;

namespace Processory.Internal {
    public class InterfaceManager {
        private readonly ProcessoryClient processoryClient;
        private readonly ILogger logger;

        public InterfaceManager(ProcessoryClient processoryClient, ILoggerFactory loggerFactory) {
            logger = loggerFactory.CreateLogger<InterfaceManager>();
            this.processoryClient = processoryClient;
        }

        public void Run(string windowName) {
            try {
                logger.LogDebug("Starting WindowService");
                if (MoveMouseToCenterOfMonitor(windowName)) {
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

        public bool MoveMouseToCenterOfMonitor(string windowTitle) {
            var handle = processoryClient.ProcessService.ProcessHandle!.MainWindowHandle;
            var mainWindowTitle = processoryClient.ProcessService.ProcessHandle.MainWindowTitle;
            var millisecondsTimeout = 1000;

            if (!IsValidWindow(handle, mainWindowTitle, windowTitle)) {
                return false;
            }

            RestoreWindow(handle);
            Thread.Sleep(millisecondsTimeout);
            SetWindowToForeground(handle);
            Thread.Sleep(millisecondsTimeout);
            // MaximizeWindow(handle);
            SnapWindowToRightHalf(handle);
            Thread.Sleep(millisecondsTimeout);

            var monitorInfo = GetMonitorInfo(handle);
            if (monitorInfo == null) {
                return false;
            }

            // MoveMouseToCenter(monitorInfo.Value);
            MoveMouseToCenterOfRightHalf(monitorInfo.Value);
            return true;
        }

        private bool IsValidWindow(IntPtr handle, string mainWindowTitle, string expectedTitle) {
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

        private void RestoreWindow(IntPtr handle) {
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

        private void SetWindowToForeground(IntPtr handle) {
            if (User32.SetForegroundWindow(handle)) {
                logger.LogDebug("Window set to foreground successfully.");
            }
            else {
                throw new Exception("Failed to set the window to foreground.");
            }
        }

        private void MaximizeWindow(IntPtr handle) {
            if (!User32.IsZoomed(handle)) {
                logger.LogDebug("Window is not maximized, attempting to maximize.");
                User32.ShowWindow(handle, (int)ShowCommands.SW_MAXIMIZE);
            }
            else {
                logger.LogDebug("Window is already maximized.");
            }
        }

        private void SnapWindowToRightHalf(IntPtr handle) {
            MonitorInfo? monitorInfo = GetMonitorInfo(handle);
            if (monitorInfo == null) {
                logger.LogError("Failed to get monitor info for snapping window.");
                return;
            }

            int monitorWidth = monitorInfo.Value.rcMonitor.Right - monitorInfo.Value.rcMonitor.Left;
            int monitorHeight = monitorInfo.Value.rcMonitor.Bottom - monitorInfo.Value.rcMonitor.Top;
            int rightHalfWidth = monitorWidth / 2;
            int rightHalfLeft = monitorInfo.Value.rcMonitor.Left + rightHalfWidth;

            if (!User32.MoveWindow(handle, rightHalfLeft, monitorInfo.Value.rcMonitor.Top, rightHalfWidth, monitorHeight, true)) {
                int errorCode = Marshal.GetLastWin32Error();
                logger.LogWarning("Failed to snap window to the right half of the screen. Error code: {ErrorCode}", errorCode);
            }
            else {
                logger.LogDebug("Window snapped to the right half of the screen successfully.");
            }
        }

        private MonitorInfo? GetMonitorInfo(IntPtr handle) {
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

        private static void MoveMouseToCenter(MonitorInfo monitorInfo) {
            const int Half = 2;
            int centerX = monitorInfo.rcMonitor.Left +
                          ((monitorInfo.rcMonitor.Right - monitorInfo.rcMonitor.Left) / Half);
            int centerY = monitorInfo.rcMonitor.Top + ((monitorInfo.rcMonitor.Bottom - monitorInfo.rcMonitor.Top) / Half);

            User32.SetCursorPos(centerX, centerY);
        }

        private void MoveMouseToCenterOfRightHalf(MonitorInfo monitorInfo) {
            int monitorWidth = monitorInfo.rcMonitor.Right - monitorInfo.rcMonitor.Left;
            int rightHalfWidth = monitorWidth / 2;
            int rightHalfLeft = monitorInfo.rcMonitor.Left + rightHalfWidth;
            int centerX = rightHalfLeft + (rightHalfWidth / 2);
            int centerY = monitorInfo.rcMonitor.Top + ((monitorInfo.rcMonitor.Bottom - monitorInfo.rcMonitor.Top) / 2);

            User32.SetCursorPos(centerX, centerY);
            logger.LogDebug("Mouse moved to center of the right half of the screen successfully.");
        }
    }
}