namespace Processory.Native;

public static class Flags {
    // [Flags]
    public enum ProcessAccess : uint {
        /// <summary>Required to terminate a process using TerminateProcess.</summary>
        Terminate = 0x00000001,
        /// <summary>Required to create a thread.</summary>
        CreateThread = 0x00000002,
        /// <summary>Required to perform an operation on the address space of a process (e.g., VirtualProtectEx and WriteProcessMemory).</summary>
        VirtualMemoryOperation = 0x00000008,
        /// <summary>Required to read memory in a process using ReadProcessMemory.</summary>
        VirtualMemoryRead = 0x00000010,
        /// <summary>Required to write to memory in a process using WriteProcessMemory.</summary>
        VirtualMemoryWrite = 0x00000020,
        /// <summary>Required to duplicate a handle using DuplicateHandle.</summary>
        DuplicateHandle = 0x00000040,
        /// <summary>Required to create a process.</summary>
        CreateProcess = 0x000000080,
        /// <summary>Required to set memory limits using SetProcessWorkingSetSize.</summary>
        SetQuota = 0x00000100,
        /// <summary>Required to set certain information about a process, such as its priority class (see SetPriorityClass).</summary>
        SetInformation = 0x00000200,
        /// <summary>Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken).</summary>
        QueryInformation = 0x00000400,
        /// <summary>Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass, IsProcessInJob).</summary>
        QueryLimitedInformation = 0x00001000,
        /// <summary>Required to wait for the process to terminate using the wait functions.</summary>
        Synchronize = 0x00100000,
        /// <summary>All possible access rights for a process object.</summary>
        All = 0x001F0FFF
    }

    //Memory State
    //https://msdn.microsoft.com/en-us/library/windows/desktop/aa366775(v=vs.85).aspx
    [Flags]
    public enum MemoryState : uint {
        /// <summary>Disables all access to the committed region of pages. An attempt to read from, write to, or execute the committed region results in an access violation.</summary>
        PAGE_NO_ACCESS = 0x01,
        /// <summary>Enables both read and write access to the committed region of pages.</summary>
        PAGE_READWRITE = 0x04,
        /// <summary>Pages in the region become guard pages. Any attempt to access a guard page causes the system to raise a STATUS_GUARD_PAGE_VIOLATION exception and turn off the guard page status.</summary>
        PAGE_GUARD = 0x100,
        /// <summary>Indicates committed pages for which physical storage has been allocated, either in memory or in the paging file on disk.</summary>
        MEM_COMMIT = 0x1000
    }
}