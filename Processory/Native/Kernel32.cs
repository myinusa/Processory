using System.Runtime.InteropServices;
using static Processory.Native.Structures;

namespace Processory.Native;

public static class MethodsNative {
    // [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    // [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    // internal static extern bool ReadProcessMemory(IntPtr hProcess, ulong lpBaseAddress, byte[] lpBuffer, int nSize, ref int lpNumberOfBytesRead);

    // [DllImport("kernel32.dll")]
    // internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr buffer, int size, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBuffer, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer, uint nBytesRead, out IntPtr pcBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBuffer, IntPtr buffer, uint nBytesRead, out IntPtr pcBytesRead);

    // [DllImport("kernel32.dll")]
    // internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr buffer, int size, out IntPtr lpNumberOfBytesRead);

    // [DllImport("kernel32.dll")]
    // internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref IntPtr buffer, int size, out IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, UIntPtr lpBuffer, UIntPtr nSize, out UIntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, out UIntPtr lpNumberOfBytesRead);


    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ReadProcessMemory(
        IntPtr hProcess,
        UIntPtr lpBaseAddress,
        ref byte lpBuffer,
        UIntPtr nSize,
        out UIntPtr lpNumberOfBytesRead
    );
}

public static partial class NativeMethods {

    [DllImport("kernel32.dll")]
    internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr dwSize, ref int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll")]
    internal static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, int dwLength);


    [DllImport("kernel32.dll")]
    internal static extern IntPtr OpenProcess(Flags.ProcessAccess processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll")]
    internal static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

    [DllImport("kernel32.dll")]
    internal static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

    [DllImport("kernel32.dll")]
    internal static extern int VirtualQueryEx(IntPtr hProcess, UIntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

    // [DllImport("kernel32.dll")]
    // internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr buffer, int size, IntPtr lpNumberOfBytesRead);

    // [DllImport("kernel32.dll")]
    // internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr buffer, int size, ref IntPtr lpNumberOfBytesRead);

    // [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
    // [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    // public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, nuint nSize, out IntPtr lpNumberOfBytesRead);

}
