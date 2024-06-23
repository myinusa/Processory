using Processory.Memory;
using Processory.Services;

namespace Processory;

public class ProcessoryClient {
    public MemoryReader MemoryReader { get; set; }
    public ProcessService ProcessService { get; set; }
    public string ProcessName { get; }
    public nint ProcessHandle { get; }

    public ProcessoryClient(string processName) {
        ProcessName = processName;
        ProcessService = new ProcessService(this);
        MemoryReader = new MemoryReader(this);
        ProcessHandle = ProcessService.GetProcessHandle();
    }


}