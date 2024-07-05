using Processory.Helpers;
using Processory.Internal;
using Processory.Pointers;
using Processory.Services;
using Microsoft.Extensions.Logging;
using Processory.Utilities;

namespace Processory;

public class ProcessoryClient {
    public MemoryReader MemoryReader { get; set; }
    public ProcessService ProcessService { get; set; }
    public InterfaceManager InterfaceManager { get; set; }
    public CSVDataOffsetManager CSVDataOffsetManager { get; set; }
    public string ProcessName { get; }
    public nint ProcessHandle { get; }
    private readonly ILogger logger;
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

    public AddressService AddressService {
        get {
            if (ProcessHandle == 0) {
                return null;
            }
            else {
                return new AddressService(this);
            }
        }
    }

    public ProcessoryClient(string processName, ILoggerFactory loggerFactory = null, string nameOfCSV = "") {
        logger = loggerFactory?.CreateLogger<ProcessoryClient>();
        ProcessName = processName;
        ProcessService = new ProcessService(this, loggerFactory);
        CSVDataOffsetManager = new CSVDataOffsetManager(loggerFactory, nameOfCSV);
        InterfaceManager = new InterfaceManager(this, loggerFactory);
        MemoryReader = new MemoryReader(this);
        ProcessHandle = ProcessService.GetProcessHandle();
    }


}