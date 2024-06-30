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
            var handle = processoryClient.ProcessService.ProcessHandle.MainWindowHandle;
            var mainWindowTitle = processoryClient.ProcessService.ProcessHandle.MainWindowTitle;

            if (!IsValidWindow(handle, mainWindowTitle, windowTitle)) {
                return false;
            }

            RestoreWindow(handle);
            SetWindowToForeground(handle);
            MaximizeWindow(handle);

            var monitorInfo = GetMonitorInfo(handle);
            if (monitorInfo == null) {
                return false;
            }

            MoveMouseToCenter(monitorInfo.Value);
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
            if (NativeMethods.IsIconic(handle)) {
                logger.LogDebug("Window is minimized, attempting to restore.");
                if (NativeMethods.ShowWindow(handle, (int)ShowCommands.SW_RESTORE)) {
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
            if (NativeMethods.SetForegroundWindow(handle)) {
                logger.LogDebug("Window set to foreground successfully.");
            }
            else {
                logger.LogWarning("Failed to set the window to foreground.");
            }
        }

        private void MaximizeWindow(IntPtr handle) {
            if (!NativeMethods.IsZoomed(handle)) {
                logger.LogDebug("Window is not maximized, attempting to maximize.");
                NativeMethods.ShowWindow(handle, (int)ShowCommands.SW_MAXIMIZE);
            }
            else {
                logger.LogDebug("Window is already maximized.");
            }
        }

        private NativeMethods.MonitorInfo? GetMonitorInfo(IntPtr handle) {
            nint monitor = NativeMethods.MonitorFromWindow(handle, 0);
            NativeMethods.MonitorInfo mi = new NativeMethods.MonitorInfo {
                cbSize = (uint)Marshal.SizeOf<NativeMethods.MonitorInfo>()
            };

            if (!NativeMethods.GetMonitorInfo(monitor, ref mi)) {
                logger.LogError("Failed to get monitor info.");
                return null;
            }

            return mi;
        }

        private void MoveMouseToCenter(NativeMethods.MonitorInfo monitorInfo) {
            const int Half = 2;
            int centerX = monitorInfo.rcMonitor.Left + (monitorInfo.rcMonitor.Right - monitorInfo.rcMonitor.Left) / Half;
            int centerY = monitorInfo.rcMonitor.Top + (monitorInfo.rcMonitor.Bottom - monitorInfo.rcMonitor.Top) / Half;

            NativeMethods.SetCursorPos(centerX, centerY);
        }
    }
}