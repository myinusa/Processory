using System.Runtime.InteropServices;
using Processory.Native.User32;

namespace Processory.Native;
public static class CursorManagement {
    public struct Point {
        public int X { get; set; }
        public int Y { get; set; }
    }

    // [DllImport("user32.dll")]
    // public static extern bool GetCursorPos(out Point lpPoint);

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(ref Point lpPoint);

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    public static extern bool ClientToScreen(IntPtr hWnd, out Point lpPoint);

    [DllImport("user32.dll")]
    public static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);
}
