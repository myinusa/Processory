using System.Runtime.InteropServices;
using System.Text;

namespace Processory.Native;

public static class DbgHelpInterop {
    [DllImport("dbghelp.dll", SetLastError = true, PreserveSig = true, CharSet = CharSet.Unicode)]
    public static extern int UnDecorateSymbolName(
        [In] string decoratedName,
        [Out] StringBuilder unDecoratedName,
        [In][MarshalAs(UnmanagedType.U4)] int undecoratedLength,
        [In][MarshalAs(UnmanagedType.U4)] int flags
    );
}