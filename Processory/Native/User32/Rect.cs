namespace Processory.Native.User32;

// Define the RECT struct for the GetClientRect function
public struct Rect {
    public int Left { get; set; }

    public int Top { get; set; }

    public int Right { get; set; }

    public int Bottom { get; set; }

    public int Width => Right - Left;
    public int Height => Bottom - Top;
}
