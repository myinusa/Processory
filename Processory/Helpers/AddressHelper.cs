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

    public bool IsValidPointer(nuint address, out nuint endAddress) {
        endAddress = nuint.Zero;

        // foreach (var item in ProcessHandleCollection) {
        MEMORY_BASIC_INFORMATION mbi = new();
        if (NativeMethods.VirtualQueryEx(ProcessoryClient.ProcessHandle, address, out mbi, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))) == 0)
            return false;

        bool isMemoryValid = mbi.State == (uint)Flags.MemoryState.MEM_COMMIT &&
                             (mbi.Protect == (uint)MemoryProtection.ReadWrite ||
                              mbi.Protect == (uint)MemoryProtection.ReadOnly ||
                              mbi.Protect == (uint)MemoryProtection.ExecuteRead ||
                              mbi.Protect == (uint)MemoryProtection.ExecuteReadWrite);

        // Check if the address is within the region.
        bool isAddressInRange = address.ToUInt64() >= mbi.BaseAddress.ToUInt64() &&
                                address.ToUInt64() <= mbi.BaseAddress.ToUInt64() + mbi.RegionSize.ToUInt64();

        if (isMemoryValid && isAddressInRange) {
            // Calculate the end address of the memory region
            ulong regionEnd = mbi.BaseAddress.ToUInt64() + mbi.RegionSize.ToUInt64();
            endAddress = new nuint(regionEnd);
            return true;
        }
        // }

        return false;
    }
}