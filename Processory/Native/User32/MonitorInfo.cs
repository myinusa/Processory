namespace Processory.Native.User32;

public static partial class NativeMethods {
    public struct MonitorInfo {
        public uint cbSize;
        public Rect rcMonitor;
        public Rect rcWork;
        public uint dwFlags;
    }
}
