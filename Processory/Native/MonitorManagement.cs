using System.Runtime.InteropServices;
using Processory.Native.User32;

namespace Processory.Native;
public static class MonitorManagement {
    [DllImport("user32.dll")]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);

    [DllImport("user32.dll")]
    public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);
}