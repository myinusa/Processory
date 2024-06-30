using System.Runtime.InteropServices;

namespace Processory.Native.User32;

public static class User32 {
    // Import the keybd_event function from user32.dll
    [DllImport("user32.dll", SetLastError = true)]
    public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
    // Virtual key code for F5
    public const byte VK_F5 = 0x74;
    // Key event constants
    public const uint KEYEVENTF_KEYDOWN = 0x0000;
    public const uint KEYEVENTF_KEYUP = 0x0002;

    [DllImport("user32.DLL")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll")]
    public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    public const int SM_CXVIRTUALSCREEN = 78;
    public const int SM_CYVIRTUALSCREEN = 79;

    // Define the POINT struct for the ClientToScreen function
    public struct Point {
        public int X;
        public int Y;
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

    public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
    public const uint MOUSEEVENTF_MOVE = 0x0001;

    public const int SW_MAXIMIZE = 3;

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);

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
