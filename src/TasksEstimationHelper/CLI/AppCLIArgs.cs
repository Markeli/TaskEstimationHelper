using Curiosity.Hosting;
using EntryPoint;

namespace TasksEstimationHelper.CLI;

/// <summary>
/// CLI arguments.
/// </summary>
public class AppCLIArgs : CuriosityCLIArguments
{
    /// <summary>
    /// Path to csv file with progress report.
    /// </summary>
    [OptionParameter("progress-report")]
    public string ProgressReportPath { get; set; } = null!;
}
