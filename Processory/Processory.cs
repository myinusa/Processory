using Processory.Services;

namespace Processory;

public class ProcessoryClient { // Renamed from Processory to ProcessoryClient
    //private readonly nint _processHandle;
    public readonly string _processName;
    public ProcessService ProcessService;

    public ProcessoryClient(string processName) {
        _processName = processName;
        ProcessService = new ProcessService(this);
        // _processHandle = GetProcessHandle();
    }
}