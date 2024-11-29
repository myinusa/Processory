namespace Processory.Native.User32;

public struct MonitorInfo {
    public uint CbSize;
    public Rect rcMonitor;
    public Rect RcWork;
    public uint DwFlags;
}
