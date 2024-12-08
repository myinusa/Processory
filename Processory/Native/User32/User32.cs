using System.Runtime.InteropServices;

namespace Processory.Native.User32;

public static class User32 {
    // Import the keybd_event function from user32.dll
    [DllImport("user32.dll", SetLastError = true)]
    public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

    [DllImport("user32.DLL")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll")]
    public static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    // Define the POINT struct for the ClientToScreen function
    public struct Point {
        public int X { get; set; }
        public int Y { get; set; }
    }

    [DllImport("user32.dll")]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);

    [DllImport("user32.dll")]
    public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    [DllImport("user32.dll")]
    public static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Point lpPoint);

    [DllImport("user32.dll")]
    public static extern bool ClientToScreen(IntPtr hWnd, out Point lpPoint);


    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

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
    public static extern bool BringWindowToTop(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);


    /// <summary>
    /// Determines whether the specified window is minimized (iconic).
    /// </summary>
    /// <param name="hWnd">A handle to the window to be checked.</param>
    /// <returns>
    /// Returns true if the window is minimized; otherwise, false.
    /// </returns>
    /// <remarks>
    /// This function can be used to check the state of a window before performing operations
    /// that require the window to be in a specific state.
    /// </remarks>
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsIconic(IntPtr hWnd);

    #region ShowWindow
    /// <summary>
    /// Sets the specified window's show state.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="nCmdShow">
    /// Controls how the window is to be shown.
    /// This parameter is ignored the first time an application calls ShowWindow, if the program that launched the application provides a STARTUPINFO structure.
    /// Otherwise, the first time ShowWindow is called, the value should be the value obtained by the WinMain function in its nCmdShow parameter.
    /// In subsequent calls, this parameter can be one of the following values.
    /// </param>
    /// <returns>
    /// If the window was previously visible, the return value is nonzero.
    /// If the window was previously hidden, the return value is zero.
    /// </returns>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    #endregion

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsZoomed(IntPtr hWnd);
}
