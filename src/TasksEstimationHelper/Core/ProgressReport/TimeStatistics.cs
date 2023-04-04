namespace TasksEstimationHelper.Core.ProgressReport;

/// <summary>
/// Time statistics of a time, person, etc.
/// </summary>
public readonly struct TimeStatistics
{
    /// <summary>
    /// Original task's estimation. 
    /// </summary>
    public int EstimationMin { get; }

    /// <summary>
    /// Time spent on a task for a report's period. 
    /// </summary>
    public int SpentTimeMin { get; }

    /// <summary>
    /// Delta between <see cref="EstimationMin"/> and <see cref="SpentTimeMin"/>.
    /// </summary>
    public int EstimationSpentDeltaMin { get; }

    /// <summary>
    /// Ratio of <see cref="EstimationMin"/> and <see cref="SpentTimeMin"/>
    /// </summary>
    public double EstimationTimeRatio { get; }

    /// <inheritdoc cref="TimeStatistics"/>
    public TimeStatistics(
        int estimationMin,
        int spentTimeMin) : this()
    {
        EstimationMin = estimationMin;
        SpentTimeMin = spentTimeMin;

        EstimationSpentDeltaMin =  spentTimeMin - estimationMin;

        if (EstimationMin == 0)
        {
            EstimationTimeRatio = 0;
        }
        else
        {
            EstimationTimeRatio = ((double)SpentTimeMin - EstimationMin) / EstimationMin;
        }
    }
}
