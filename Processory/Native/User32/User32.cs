using System.Runtime.InteropServices;

namespace Processory.Native.User32;

public static partial class NativeMethods {
    [LibraryImport("user32.DLL", StringMarshalling = StringMarshalling.Utf16)]
    public static partial IntPtr FindWindow(string lpClassName, string lpWindowName);

    [LibraryImport("user32.dll")]
    public static partial int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll")]
    public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    [LibraryImport("user32.dll")]
    public static partial void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool SetForegroundWindow(IntPtr hWnd);

    public const int SM_CXVIRTUALSCREEN = 78;
    public const int SM_CYVIRTUALSCREEN = 79;

    // Define the POINT struct for the ClientToScreen function
    public struct Point {
        public int X;
        public int Y;
    }

    [DllImport("user32.dll")]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);

    [LibraryImport("user32.dll")]
    public static partial IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

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

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsIconic(IntPtr hWnd);

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
    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);
    #endregion

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsZoomed(IntPtr hWnd);
}
