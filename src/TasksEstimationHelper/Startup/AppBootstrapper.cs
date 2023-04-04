using Curiosity.Configuration;
using Curiosity.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TasksEstimationHelper.CLI;
using TasksEstimationHelper.Configuration;
using TasksEstimationHelper.Core.ProgressReport;

namespace TasksEstimationHelper.Startup;

public class AppBootstrapper : CuriosityToolAppBootstrapper<AppCLIArgs, AppConfiguration>
{
    public AppBootstrapper()
    {
        ConfigureServices((services, _) =>
        {
            services.AddSingleton<YouTrackProgressReportEstimationCorrector>();
            services.AddSingleton<ProgressReportStatisticsConsolePrinter>();
        });
    }

    protected override async Task<int> ExecuteAsync(
        IServiceProvider serviceProvider,
        string[] rawArguments,
        AppCLIArgs arguments,
        AppConfiguration configuration,
        CancellationToken cancellationToken = new())
    {
        if (String.IsNullOrWhiteSpace(arguments.ProgressReportPath))
            throw new ArgumentException("Path to report wasn't specified");

        var statisticsCorrector = serviceProvider.GetRequiredService<YouTrackProgressReportEstimationCorrector>();
        var reportStatistics = await statisticsCorrector.BuildReportStatisticsAsync(
            arguments.ProgressReportPath,
            cancellationToken);

        var printer = serviceProvider.GetRequiredService<ProgressReportStatisticsConsolePrinter>();
        printer.Print(reportStatistics);

        return CuriosityExitCodes.Success;
    }
}
