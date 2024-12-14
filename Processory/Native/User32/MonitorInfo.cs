namespace Processory.Native.User32;

public struct MonitorInfo {
    public uint CbSize { get; set; }
    public Rect RcMonitor { get; set; }
    public Rect RcWork { get; set; }
    public uint DwFlags { get; set; }
}
