using TasksEstimationHelper.Guards;

namespace TasksEstimationHelper.Core.ProgressReport;

/// <summary>
/// Statistics build from progress report.
/// </summary>
public class ProgressReportStatistics
{
    /// <summary>
    /// Statistics of each person.
    /// </summary>
    public IReadOnlyList<PersonStatistics> PersonsStatistics { get; }

    /// <summary>
    /// Total estimation-spent ratio statistics. 
    /// </summary>
    public EstimationSpentRatioStatistics TotalEstimationSpentRatioStatistics { get; }

    /// <summary>
    /// Average estimations-spent ration by person's average.
    /// </summary>
    public double AverageEstimationSpentRatioPerMember { get; }

    /// <inheritdoc cref="ProgressReportStatistics"/>
    public ProgressReportStatistics(
        IReadOnlyList<PersonStatistics> personsStatistics,
        EstimationSpentRatioStatistics totalEstimationSpentRatioStatistics,
        double averageEstimationSpentRatioPerMember)
    {
        personsStatistics.AssertNotNull(nameof(personsStatistics));

        PersonsStatistics = personsStatistics;
        TotalEstimationSpentRatioStatistics = totalEstimationSpentRatioStatistics;
        AverageEstimationSpentRatioPerMember = averageEstimationSpentRatioPerMember;
    }
}
