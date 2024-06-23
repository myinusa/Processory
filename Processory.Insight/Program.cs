using System.Diagnostics;

namespace Processory.Insight;

public class Program {

    public static void Main() {
        var GivenName = "fm";
        var processory = new ProcessoryClient(GivenName);
        processory.ProcessService.LogProcessAndModuleInfo();
    }

}



