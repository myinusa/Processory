using System.Runtime.InteropServices;

namespace Processory.Native.Window;

public static class WindowState {
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindowEnabled(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsHungAppWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsIconic(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsZoomed(IntPtr hWnd);

    public static string GetWindowStatus(IntPtr hWnd) {
        if (!IsWindow(hWnd)) {
            return "Invalid";
        }
        if (!IsWindowVisible(hWnd)) {
            return "Hidden";
        }
        if (!IsWindowEnabled(hWnd)) {
            return "Disabled";
        }
        if (IsHungAppWindow(hWnd)) {
            return "Not Responding";
        }
        if (IsIconic(hWnd)) {
            return "Minimized";
        }
        if (IsZoomed(hWnd)) {
            return "Maximized";
        }
        return "Restored";
    }
}