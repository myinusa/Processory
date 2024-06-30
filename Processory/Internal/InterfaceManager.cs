//using System;
//using System.Runtime.InteropServices;
//using FMProcessInsight.Logging;
//using Processory;
//using Processory.Native.User32;
//using Serilog;

//namespace FMProcessInsight {
//    public class WindowService {
//        private readonly ProcessoryClient processoryClient;
//        private static ILogger Log => Serilog.Log.ForContext<WindowService>();

//        public WindowService(ProcessoryClient processoryClient) {
//            this.processoryClient = processoryClient;
//        }

//        public void Run() {
//            try {
//                Log.Here().Information("Starting WindowService");
//                if (MoveMouseToCenterOfMonitor("Football Manager 2022")) {
//                    Log.Here().Information("Mouse moved to center of monitor successfully.");
//                }
//                else {
//                    Log.Here().Warning("Failed to move mouse to center of monitor.");
//                }
//                Log.Here().Information("Finished WindowService");
//            }
//            catch (Exception e) {
//                Log.Fatal(e, "An unhandled exception occurred while starting the application.");
//            }
//        }

//        public bool MoveMouseToCenterOfMonitor(string windowTitle) {
//            var handle = processoryClient.ProcessService.ProcessHandle.MainWindowHandle;
//            var mainWindowTitle = processoryClient.ProcessService.ProcessHandle.MainWindowTitle;

//            if (!IsValidWindow(handle, mainWindowTitle, windowTitle)) {
//                return false;
//            }

//            RestoreWindow(handle);
//            SetWindowToForeground(handle);
//            MaximizeWindow(handle);

//            var monitorInfo = GetMonitorInfo(handle);
//            if (monitorInfo == null) {
//                return false;
//            }

//            MoveMouseToCenter(monitorInfo.Value);
//            return true;
//        }

//        private bool IsValidWindow(IntPtr handle, string mainWindowTitle, string expectedTitle) {
//            if (mainWindowTitle != expectedTitle) {
//                Log.Here().Debug("MainWindowTitle: {Title}", mainWindowTitle);
//                return false;
//            }

//            if (handle == IntPtr.Zero) {
//                Log.Here().Error("Invalid window handle.");
//                return false;
//            }

//            return true;
//        }

//        private void RestoreWindow(IntPtr handle) {
//            if (NativeMethods.IsIconic(handle)) {
//                Log.Here().Debug("Window is minimized, attempting to restore.");
//                if (NativeMethods.ShowWindow(handle, (int)ShowCommands.SW_RESTORE)) {
//                    Log.Here().Information("Window restored successfully.");
//                }
//                else {
//                    int errorCode = Marshal.GetLastWin32Error();
//                    Log.Here().Warning("Failed to restore the window. Error code: {ErrorCode}", errorCode);
//                }
//            }
//            else {
//                Log.Here().Debug("Window is not minimized.");
//            }
//        }

//        private void SetWindowToForeground(IntPtr handle) {
//            if (NativeMethods.SetForegroundWindow(handle)) {
//                Log.Here().Information("Window set to foreground successfully.");
//            }
//            else {
//                Log.Here().Warning("Failed to set the window to foreground.");
//            }
//        }

//        private void MaximizeWindow(IntPtr handle) {
//            if (!NativeMethods.IsZoomed(handle)) {
//                Log.Here().Debug("Window is not maximized, attempting to maximize.");
//                NativeMethods.ShowWindow(handle, (int)ShowCommands.SW_MAXIMIZE);
//            }
//            else {
//                Log.Here().Debug("Window is already maximized.");
//            }
//        }

//        private NativeMethods.MonitorInfo? GetMonitorInfo(IntPtr handle) {
//            nint monitor = NativeMethods.MonitorFromWindow(handle, 0);
//            NativeMethods.MonitorInfo mi = new NativeMethods.MonitorInfo {
//                cbSize = (uint)Marshal.SizeOf<NativeMethods.MonitorInfo>()
//            };

//            if (!NativeMethods.GetMonitorInfo(monitor, ref mi)) {
//                Log.Here().Error("Failed to get monitor info.");
//                return null;
//            }

//            return mi;
//        }

//        private void MoveMouseToCenter(NativeMethods.MonitorInfo monitorInfo) {
//            const int Half = 2;
//            int centerX = monitorInfo.rcMonitor.Left + (monitorInfo.rcMonitor.Right - monitorInfo.rcMonitor.Left) / Half;
//            int centerY = monitorInfo.rcMonitor.Top + (monitorInfo.rcMonitor.Bottom - monitorInfo.rcMonitor.Top) / Half;

//            NativeMethods.SetCursorPos(centerX, centerY);
//        }
//    }
//}