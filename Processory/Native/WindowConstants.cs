namespace Processory.Native;

public static class WindowConstants {
    public const uint SWP_NOMOVE = 0x0002;
    public const uint SWP_NOSIZE = 0x0001;
    public const uint SWP_SHOWWINDOW = 0x0040;
    public const int SW_RESTORE = 9;
    public const int SW_MAXIMIZE = 3;
    public static readonly IntPtr HWND_TOP = new(0);
}