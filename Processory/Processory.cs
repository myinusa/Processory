using Microsoft.Extensions.Logging;
using Processory.Helpers;
using Processory.Internal;
using Processory.Pointers;
using Processory.Services;
using Processory.Utilities;

namespace Processory {
    /// <summary>
    /// Represents a client for interacting with a process.
    /// </summary>
    public class ProcessoryClient {
        public MemoryReader MemoryReader { get; private set; }
        public ProcessService ProcessService { get; private set; }
        public InterfaceManager InterfaceManager { get; private set; }
        public CSVDataOffsetManager CSVDataOffsetManager { get; private set; }
        public string ProcessName { get; }
        public nint ProcessHandle { get; private set; }
        private readonly ILogger logger;

        public PointerChainFollower PointerChainFollower => GetService<PointerChainFollower>();
        public RunTimeTypeInformation RunTimeTypeInformation => GetService<RunTimeTypeInformation>();
        public AddressHelper AddressHelper => GetService<AddressHelper>();
        public AddressService AddressService => GetService<AddressService>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessoryClient"/> class.
        /// </summary>
        /// <param name="processName">The name of the process to interact with.</param>
        /// <param name="loggerFactory">The logger factory for creating loggers.</param>
        /// <param name="nameOfCSV">The name of the CSV file for data offsets.</param>
        public ProcessoryClient(string processName, ILoggerFactory loggerFactory = null, string nameOfCSV = "") {
            logger = loggerFactory?.CreateLogger<ProcessoryClient>();
            ProcessName = processName;
            ProcessService = new ProcessService(this, loggerFactory);
            CSVDataOffsetManager = new CSVDataOffsetManager(loggerFactory, nameOfCSV);
            InterfaceManager = new InterfaceManager(this, loggerFactory);
            MemoryReader = new MemoryReader(this, loggerFactory);
            ProcessHandle = ProcessService.GetProcessHandle();
        }

        /// <summary>
        /// Gets the service instance if the process handle is valid.
        /// </summary>
        /// <typeparam name="T">The type of the service to get.</typeparam>
        /// <returns>The service instance or null if the process handle is invalid.</returns>
        private T GetService<T>() where T : class {
            if (ProcessHandle == 0) {
                return null;
            }

            return typeof(T) switch {
                var t when t == typeof(PointerChainFollower) => new PointerChainFollower(this) as T,
                var t when t == typeof(RunTimeTypeInformation) => new RunTimeTypeInformation(this) as T,
                var t when t == typeof(AddressHelper) => new AddressHelper(this) as T,
                var t when t == typeof(AddressService) => new AddressService(this) as T,
                _ => throw new InvalidOperationException($"Unsupported service type: {typeof(T).Name}")
            };
        }
    }
}