using System.Runtime.InteropServices;

namespace Processory.Native.User32;

public static class User32 {
    [DllImport("user32.dll")]
    public static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);
}