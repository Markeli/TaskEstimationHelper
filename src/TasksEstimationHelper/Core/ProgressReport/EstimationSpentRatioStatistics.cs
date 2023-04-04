namespace TasksEstimationHelper.Core.ProgressReport;

public readonly struct EstimationSpentRatioStatistics
{
    public double AveragePerTasks { get; }
    public double AveragePerTotal { get; }
    public double Min { get; }
    public double Max { get; }
    public double Median { get; }

    public EstimationSpentRatioStatistics(
        double averagePerTasks,
        double averagePerTotal,
        double min,
        double max,
        double median)
    {
        AveragePerTasks = averagePerTasks;
        AveragePerTotal = averagePerTotal;
        Min = min;
        Max = max;
        Median = median;
    }
}
