namespace Processory.Insight;

public class Program {

    public static void Main() {
        var GivenName = "fm";
        var processory = new ProcessoryClient(GivenName);
        processory.ProcessService.LogProcessAndModuleInfo();

        var baseAddress = processory.ProcessService.ProcessHandle?.MainModule?.BaseAddress;

        if (baseAddress == null) {
            Console.WriteLine("Could not find process");
            return;
        }

        var screenClient = (ulong)(baseAddress + 0x4C7FF08);
        var deferAdd = processory.PointerChainFollower.DereferencePointer(screenClient);
        var screenClientValue = processory.MemoryReader.Read<uint>(deferAdd);

        Console.WriteLine("Screen client: {0:X}", screenClient);
        Console.WriteLine("Screen client value: {0}", screenClientValue);
    }

}



