using Microsoft.Extensions.Logging;
using Processory.Native.User32;

namespace Processory.Internal {
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

            if (!windowManager.IsValidWindow(handle, mainWindowTitle, windowTitle)) {
                logger.LogError("Invalid window handle.");
                return false;
            }

            var windowStatus = User32.GetWindowStatus(handle);
            logger.LogDebug("Window status: {WindowStatus}", windowStatus);

            // windowManager.RestoreWindow(handle);
            Thread.Sleep(MillisecondsTimeout);
            // windowManager.SetWindowToForeground(handle);
            windowManager.BringWindowToFront(handle);
            Thread.Sleep(MillisecondsTimeout);
            windowManager.SnapWindowToRightHalf(handle);
            Thread.Sleep(MillisecondsTimeout);

            var monitorInfo = windowManager.GetMonitorInfo(handle);
            if (monitorInfo == null) {
                return false;
            }

            MoveMouseToCenterOfRightHalf(monitorInfo.Value);
            return true;
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