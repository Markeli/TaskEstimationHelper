using Curiosity.Configuration;
using Curiosity.Hosting;
using TasksEstimationHelper.CLI;

namespace TasksEstimationHelper.Startup;

public class AppBootstrapper : CuriosityAppBootstrapper<AppCLIArgs, CuriosityAppConfiguration>
{
    protected override Task<int> RunInternalAsync(
        string[] rawArguments,
        AppCLIArgs arguments,
        CuriosityAppConfiguration configuration,
        IConfigurationProvider<CuriosityAppConfiguration> configurationProvider,
        string customContentRootDirectory,
        CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }
}
