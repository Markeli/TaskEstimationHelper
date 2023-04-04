using TasksEstimationHelper.Guards;

namespace TasksEstimationHelper.Core.ProgressReport;

/// <summary>
/// Statistics of a one person.
/// </summary>
/// <remarks>
/// Collects statistics of all task made by a person.
/// </remarks>
public class PersonStatistics
{
    /// <summary>
    /// Name of a person.
    /// </summary>
    public string PersonName { get; }

    /// <summary>
    /// Tasks made by a person.
    /// </summary>
    public IReadOnlyList<TaskStatistics> Tasks { get; }

    /// <summary>
    /// Aggregated total time statistics.
    /// </summary>
    public TimeStatistics TotalTimeStatistics { get; }

    /// <summary>
    /// Statistics of estimation-spent ratio.
    /// </summary>
    public EstimationSpentRatioStatistics EstimationSpentRatioStatistics { get; }

    /// <inheritdoc cref="PersonStatistics"/>
    public PersonStatistics(
        string personName,
        IReadOnlyList<TaskStatistics> tasks,
        TimeStatistics totalTimeStatistics,
        EstimationSpentRatioStatistics estimationSpentRatioStatistics)
    {
        personName.AssertNotEmpty(nameof(personName));
        tasks.AssertNotNull(nameof(tasks));

        PersonName = personName;
        Tasks = tasks;
        TotalTimeStatistics = totalTimeStatistics;
        EstimationSpentRatioStatistics = estimationSpentRatioStatistics;
    }
}
