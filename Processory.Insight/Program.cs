namespace Processory.Insight;
using Microsoft.Extensions.Logging; 

public class Program {
    private const ulong GameExceptionHandlerOffset = 0x04d50e30;
    private const ulong BaseAddress = 0x140000000;

    public static void Main() {
        var GivenName = "fm";
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        var processory = new ProcessoryClient(GivenName, loggerFactory);
        processory.ProcessService.LogProcessAndModuleInfo();

        var baseAddress = processory.ProcessService.ProcessHandle?.MainModule?.BaseAddress;

        if (baseAddress == null) {
            Console.WriteLine("Could not find process");
            return;
        }

        var addr1 = BaseAddress + GameExceptionHandlerOffset;
        nuint defrOneAdd = processory.PointerChainFollower.DereferencePointer(addr1);

        var screenClient = (ulong)(baseAddress + 0x4C7FF08);
        var deferAdd = processory.PointerChainFollower.DereferencePointer(screenClient);
        var screenClientValue = processory.MemoryReader.Read<uint>(deferAdd);

        var gameInfoAddress = processory.PointerChainFollower.ResolvePointerChain(defrOneAdd, new List<int> { 0x20 });

        Console.WriteLine("Screen client: {0:X}", screenClient);
        Console.WriteLine("Screen client value: {0}", screenClientValue);
        Console.WriteLine("Game info address: {0:X}", gameInfoAddress);
    }

}



