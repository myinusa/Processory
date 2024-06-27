using Processory.Helpers;
using Processory.Internal;
using Processory.Pointers;
using Processory.Services;

namespace Processory;

public class ProcessoryClient {
    public MemoryReader MemoryReader { get; set; }
    public ProcessService ProcessService { get; set; }
    public string ProcessName { get; }
    public nint ProcessHandle { get; }
    public PointerChainFollower PointerChainFollower {
        get {
            if (ProcessHandle == 0) {
                return null;
            }
            else {
                return new PointerChainFollower(this);
            }
        }
    }

    public RunTimeTypeInformation RunTimeTypeInformation {
        get {
            if (ProcessHandle == 0) {
                return null;
            }
            else {
                return new RunTimeTypeInformation(this);
            }
        }
    }

    public AddressHelper AddressHelper {
        get {
            if (ProcessHandle == 0) {
                return null;
            }
            else {
                return new AddressHelper(this);
            }
        }
    }

    public ProcessoryClient(string processName) {
        ProcessName = processName;
        ProcessService = new ProcessService(this);
        MemoryReader = new MemoryReader(this);
        ProcessHandle = ProcessService.GetProcessHandle();
    }


}