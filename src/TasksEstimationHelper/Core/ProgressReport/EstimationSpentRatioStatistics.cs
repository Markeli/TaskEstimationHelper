namespace TasksEstimationHelper.Core.ProgressReport;

public readonly struct EstimationSpentRatioStatistics
{
    public double AveragePerTasks { get; }
    public double AveragePerTasksOnlyEstimated { get; }
    public double AveragePerTotal { get; }
    public double AveragePerTotalOnlyEstimated { get; }
    public double Min { get; }
    public double Max { get; }
    public double Median { get; }

    public double P75 { get; }

    public double P90 { get; }

    public double P95 { get; }

    public double P99 { get; }

    public double CorrectionFactor { get; }

    public EstimationSpentRatioStatistics(
        double averagePerTasks,
        double averagePerTasksOnlyEstimated,
        double averagePerTotal,
        double averagePerTotalOnlyEstimated,
        double min,
        double max,
        double median,
        double p75,
        double p90,
        double p95,
        double p99)
    {
        AveragePerTasks = averagePerTasks;
        AveragePerTasksOnlyEstimated = averagePerTasksOnlyEstimated;
        AveragePerTotal = averagePerTotal;
        AveragePerTotalOnlyEstimated = averagePerTotalOnlyEstimated;
        Min = min;
        Max = max;
        Median = median;
        P75 = p75;
        P90 = p90;
        P95 = p95;
        P99 = p99;

        CorrectionFactor = (45 * averagePerTasksOnlyEstimated +
                            10 * averagePerTasks +
                            20 * averagePerTotalOnlyEstimated +
                            8 * averagePerTotal +
                            10 * median +
                            5 * p75 +
                            1 * P90 + 
                            1 * P95)
                           / 100;
    }
}
