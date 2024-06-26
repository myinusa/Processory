using System.Runtime.InteropServices;
using Processory.Native;
using static Processory.Native.Flags;
using static Processory.Native.Structures;

namespace Processory.Helpers;

public class AddressHelper {
    public ProcessoryClient ProcessoryClient { get; set; }

    public AddressHelper(ProcessoryClient processoryClient) {
        ProcessoryClient = processoryClient;
    }

    public bool IsValidAddress(IntPtr address) {
        int result = NativeMethods.VirtualQueryEx(ProcessoryClient.ProcessHandle, address, out MEMORY_BASIC_INFORMATION mbi, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION)));
        return result != 0 && (mbi.State & (uint)MemoryState.MEM_COMMIT) != 0 && (mbi.Protect & (uint)MemoryState.PAGE_READWRITE) != 0;
    }
}