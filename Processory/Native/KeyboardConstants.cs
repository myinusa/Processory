namespace Processory.Native;

/// <summary>
/// Contains constants for virtual-key codes and key event flags.
/// For more information, see <see href="https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes">Virtual-Key Codes</see>.
/// </summary>
public static class KeyboardConstants {
    /// <summary>
    /// Virtual-Key code for the F5 key.
    /// </summary>
    public const byte VK_F5 = 0x74;

    /// <summary>
    /// Virtual-Key code for the Control key.
    /// </summary>
    public const byte VK_CONTROL = 0x11;

    /// <summary>
    /// Virtual-Key code for the 'A' key.
    /// </summary>
    public const byte VK_A = 0x41;
    /// <summary>
    /// Virtual-Key code for the 'O' key.
    /// </summary>
    public const byte VK_O = 0x4F;

    /// <summary>
    /// Virtual-Key code for the Tab key.
    /// </summary>
    public const byte VK_TAB = 0x09;

    /// <summary>
    /// Flag for a key press event.
    /// </summary>
    public const uint KEYEVENTF_KEYDOWN = 0x0000;

    /// <summary>
    /// Flag for a key release event.
    /// </summary>
    public const uint KEYEVENTF_KEYUP = 0x0002;
}