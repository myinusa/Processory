using System.Runtime.InteropServices;
using System.Text;

namespace Processory.Native;

public static partial class NativeMethods {
    [DllImport("dbghelp.dll", SetLastError = true, PreserveSig = true, CharSet = CharSet.Unicode)]
    public static extern int UnDecorateSymbolName(
        [In] string DecoratedName,
        [Out] StringBuilder UnDecoratedName,
        [In][MarshalAs(UnmanagedType.U4)] int UndecoratedLength,
        [In][MarshalAs(UnmanagedType.U4)] int Flags
    );
}