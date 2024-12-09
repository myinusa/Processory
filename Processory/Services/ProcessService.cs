using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Processory.Native;

namespace Processory.Services;

public class ProcessService {
    private const string ProcessNotFoundMessage = "Process {0} not found";
    private const string FailedToOpenProcessMessage = "Failed to open process";
    //private const string FailedToCloseProcessMessage = "Failed to close process handle";
    public Process? ProcessHandle { get; set; }
    private readonly ProcessoryClient processoryClient;
    private readonly string processName;
    private readonly ILogger logger;

    public ProcessService(ProcessoryClient processoryClient, ILoggerFactory loggerFactory) {
        // this.logger = logger;
        logger = loggerFactory.CreateLogger<ProcessService>();
        this.processoryClient = processoryClient;
        processName = processoryClient.ProcessName;
        GetProcessHandle();
    }

    // private readonly InputSimulator inputSimulator = new();

    public static void SimulateF5KeyPress() {
        KeyboardMouseSimulator.SimulateKeyPress(KeyboardConstants.VK_F5);
    }

    public static void SimulateCtrlAPress() {
        KeyboardMouseSimulator.SimulateKeyCombo(KeyboardConstants.VK_CONTROL, KeyboardConstants.VK_A);
    }

    public static void SimulateRightClick() {
        KeyboardMouseSimulator.SimulateMouseClick(MouseEventConstants.MOUSEEVENTF_RIGHTDOWN);
    }

    public static void SimulateLeftClick() {
        KeyboardMouseSimulator.SimulateMouseClick(MouseEventConstants.MOUSEEVENTF_LEFTDOWN);
    }

    public nint GetProcessHandle() {
        OpenProcessHandler();
        if (ProcessHandle != null) {
            return NativeMethods.OpenProcess(Flags.ProcessAccess.All, false, ProcessHandle.Id);
        }
        throw new InvalidOperationException(string.Format(ProcessNotFoundMessage, processName));
    }


    private void OpenProcessHandler() {
        try {
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0) {
                ProcessHandle = processes[0];
                logger.LogDebug("Successfully opened process handle.");
            }
            else {
                throw new Exception($"Process {processName} not found");
            }
        }
        catch (Exception ex) {
            throw new Exception("Failed to open process", ex);
        }
    }
    public void CloseProcessHandler() {
        try {
            if (ProcessHandle != null) {
                var handle = NativeMethods.OpenProcess(Flags.ProcessAccess.All, false, ProcessHandle.Id);
                if (handle != nint.Zero) {
                    NativeMethods.CloseHandle(handle);
                }
                ProcessHandle = null;
            }
        }
        catch (Exception ex) {
            throw new InvalidOperationException(FailedToOpenProcessMessage, ex);
        }
    }

    public void LogProcessAndModuleInfo() {
        if (ProcessHandle != null) {
            LogProcessInfo();
            LogModuleInfo();
        }
        else {
            Console.WriteLine(string.Format(ProcessNotFoundMessage, processName));
        }
    }

    private void LogProcessInfo() {
        if (ProcessHandle == null) {
            return;
        }
        Console.WriteLine($"Process: {ProcessHandle.ProcessName} - {ProcessHandle.Id}");
    }

    private void LogModuleInfo() {
        IntPtr baseAddress = GetModuleBaseAddress();
        int moduleSize = GetModuleSize();
        IntPtr endAddress = GetModuleEndAddress();

        if (baseAddress != IntPtr.Zero && moduleSize > 0) {
            Console.WriteLine(
                "Module: {0}, Base Address: {1:X8}, End Address: {2:X8}",
                processName, baseAddress.ToInt64(), endAddress.ToInt64());
        }
        else {
            Console.WriteLine("Module: {0} not found or has no size.", processName);
        }
    }

    public IntPtr GetModuleBaseAddress() {
        return ProcessHandle?.MainModule?.BaseAddress ?? IntPtr.Zero;
    }

    public int GetModuleSize() {
        return ProcessHandle?.MainModule?.ModuleMemorySize ?? 0;
    }

    public IntPtr GetModuleEndAddress() {
        var mainModule = ProcessHandle?.MainModule;
        return mainModule != null ? IntPtr.Add(mainModule.BaseAddress, mainModule.ModuleMemorySize) : IntPtr.Zero;
    }
}