using Xunit.Abstractions;
using Microsoft.Extensions.Logging;
using MartinCostello.Logging.XUnit;

namespace Processory.Tests; 
public abstract class CommonTestBase {
    protected readonly ITestOutputHelper TestOutputHelper;
    protected readonly ILogger Logger;

    protected CommonTestBase(ITestOutputHelper testOutputHelper) {
        TestOutputHelper = testOutputHelper;
        var loggerFactory = LoggerFactory.Create(builder => {
            builder.SetMinimumLevel(LogLevel.Trace);
            builder.AddXUnit(testOutputHelper);
        });
        Logger = loggerFactory.CreateLogger<CommonTestBase>();
    }
}