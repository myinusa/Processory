using static Processory.Native.KeyboardConstants;
using static Processory.Native.KeyboardMouseEvents;
using static Processory.Native.MouseEventConstants;

namespace Processory.Services;

public static class KeyboardMouseSimulator {
    private const int MillisecondsTimeout = 100;

    public static void SimulateKeyPress(byte virtualKeyCode) {
        keybd_event(virtualKeyCode, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
        Thread.Sleep(MillisecondsTimeout);
        keybd_event(virtualKeyCode, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
    }

    public static void SimulateKeyCombo(byte virtualKey1, byte virtualKey2) {
        keybd_event(virtualKey1, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
        keybd_event(virtualKey2, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
        Thread.Sleep(MillisecondsTimeout);
        keybd_event(virtualKey2, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        keybd_event(virtualKey1, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
    }

    public static void SimulateMouseClick(uint mouseEventFlag) {
        mouse_event(mouseEventFlag, 0, 0, 0, UIntPtr.Zero);
        Thread.Sleep(MillisecondsTimeout);
        mouse_event((uint)(mouseEventFlag == MOUSEEVENTF_LEFTDOWN ? MOUSEEVENTF_LEFTUP : MOUSEEVENTF_RIGHTUP), 0, 0, 0, UIntPtr.Zero);
    }
}