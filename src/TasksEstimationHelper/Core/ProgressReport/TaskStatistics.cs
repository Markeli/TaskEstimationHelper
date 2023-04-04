using TasksEstimationHelper.Guards;

namespace TasksEstimationHelper.Core.ProgressReport;

/// <summary>
/// Statistics of a one task.
/// </summary>
public readonly struct TaskStatistics
{
    /// <summary>
    /// Task's code.
    /// </summary>
    /// <example>
    /// SSNG-1234, ORBIT-321.
    /// </example>
    public string TaskCode { get; }

    /// <summary>
    /// Task's name.
    /// </summary>
    public string TaskName { get; }

    /// <summary>
    /// Time statistics.
    /// </summary>
    public TimeStatistics TimeStatistics { get; }

    /// <inheritdoc cref="TaskStatistics"/>
    public TaskStatistics(
        string taskCode,
        string taskName,
        TimeStatistics timeStatistics)
    {
        taskCode.AssertNotEmpty(nameof(taskCode));
        taskName.AssertNotEmpty(nameof(taskName));

        TaskCode = taskCode;
        TaskName = taskName;
        TimeStatistics = timeStatistics;
    }
}
