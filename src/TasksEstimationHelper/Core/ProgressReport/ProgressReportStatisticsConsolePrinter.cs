using ConsoleTables;
using Curiosity.Tools;

namespace TasksEstimationHelper.Core.ProgressReport;

/// <summary>
/// Printer to print <see cref="ProgressReportStatistics"/> to console output.
/// </summary>
public class ProgressReportStatisticsConsolePrinter
{
    public void Print(ProgressReportStatistics statistics)
    {
        var table = new ConsoleTable(
            "Person/Task code",
            "Task name",
            "Estimation time (hh:mm)",
            "Spent time (hh:mm)",
            "Delta (hh:mm)",
            "Ratio (%)");

        foreach (var personsStatistic in statistics.PersonsStatistics)
        {
            // add person name header
            table.AddRow(
                personsStatistic.PersonName,
                null,
                null,
                null,
                null,
                null);

            // add tasks statistics
            for (var i = 0; i < personsStatistic.Tasks.Count; i++)
            {
                var taskStatistics = personsStatistic.Tasks[i];
                table.AddRow(
                    taskStatistics.TaskCode,
                    taskStatistics.TaskName.TrimLimit(75),
                    ToHourMinString(taskStatistics.TimeStatistics.EstimationMin),
                    ToHourMinString(taskStatistics.TimeStatistics.SpentTimeMin),
                    ToHourMinString(taskStatistics.TimeStatistics.EstimationSpentDeltaMin, true),
                    ToPercentageString(taskStatistics.TimeStatistics.EstimationTimeRatio));
            }

            // add total row
            table.AddRow(
                null,
                "TOTAL",
                ToHourMinString(personsStatistic.TotalTimeStatistics.EstimationMin),
                ToHourMinString(personsStatistic.TotalTimeStatistics.SpentTimeMin),
                ToHourMinString(personsStatistic.TotalTimeStatistics.EstimationSpentDeltaMin, true),
                ToPercentageString(personsStatistic.TotalTimeStatistics.EstimationTimeRatio));

            // add ratio statistics
            table.AddRow(
                null,
                "AVERAGE Ratio per tasks",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.AveragePerTasks));
            table.AddRow(
                null,
                "AVERAGE Ratio per tasks (only estimated!)",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.AveragePerTasksOnlyEstimated));
            table.AddRow(
                null,
                "AVERAGE Ratio per total",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.AveragePerTotal));
            table.AddRow(
                null,
                "AVERAGE Ratio per total (only estimated!)",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.AveragePerTotalOnlyEstimated));
            table.AddRow(
                null,
                "Min Ratio",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.Min));
            table.AddRow(
                null,
                "Max Ratio",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.Max));
            table.AddRow(
                null,
                "Median Ratio",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.Median));
            table.AddRow(
                null,
                "P75 Ratio",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.P75));
            table.AddRow(
                null,
                "P90 Ratio",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.P90));
            table.AddRow(
                null,
                "P95 Ratio",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.P95));
            table.AddRow(
                null,
                "P99 Ratio",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.P99));
            table.AddRow(
                null,
                "Correction factor",
                null,
                null,
                null,
                ToPercentageString(personsStatistic.EstimationSpentRatioStatistics.CorrectionFactor));
        }

        table.AddRow(
            "TOTAL BY ALL MEMBERS",
            null,
            null,
            null,
            null,
            null);


        // add ratio statistics
        table.AddRow(
            null,
            "AVERAGE Ratio per tasks",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.AveragePerTasks));
        table.AddRow(
            null,
            "AVERAGE Ratio per tasks (only estimated!)",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.AveragePerTasksOnlyEstimated));
        table.AddRow(
            null,
            "AVERAGE Ratio per total",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.AveragePerTotal));
        table.AddRow(
            null,
            "AVERAGE Ratio per total (only estimated!)",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.AveragePerTotalOnlyEstimated));
        table.AddRow(
            null,
            "AVERAGE Ratio by members average",
            null,
            null,
            null,
            ToPercentageString(statistics.AverageEstimationSpentRatioPerMember));
        table.AddRow(
            null,
            "Min Ratio",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.Min));
        table.AddRow(
            null,
            "Max Ratio",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.Max));
        table.AddRow(
            null,
            "Median Ratio",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.Median));

        table.AddRow(
            null,
            "P75 Ratio",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.P75));
        table.AddRow(
            null,
            "P90 Ratio",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.P90));
        table.AddRow(
            null,
            "P95 Ratio",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.P95));
        table.AddRow(
            null,
            "P99 Ratio",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.P99));
        table.AddRow(
            null,
            "Correction factor",
            null,
            null,
            null,
            ToPercentageString(statistics.TotalEstimationSpentRatioStatistics.CorrectionFactor));
        table.Write();
    }

    private static string ToHourMinString(int durationMin, bool showSign = false)
    {
        var isPositive = durationMin > 0 && showSign;
        var isNegative = durationMin < 0 && showSign;

        durationMin = Math.Abs(durationMin);
        var hours = durationMin / 60;
        durationMin -= hours * 60;
        return $"{(isPositive ? "+" : "")}{(isNegative ? "-" : "")}{hours:00}:{durationMin:00}";
    }

    private static string ToPercentageString(double percentage)
    {
        var isPositive = percentage > 0;
        var isNegative = percentage < 0;

        return $"{(isPositive ? "+" : "")}{(isNegative ? "-" : "")}{Math.Abs(percentage):P}";
    }
}
