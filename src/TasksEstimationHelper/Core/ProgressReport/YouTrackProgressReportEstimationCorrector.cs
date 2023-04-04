using Curiosity.Tools;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using TasksEstimationHelper.Guards;

namespace TasksEstimationHelper.Core.ProgressReport;

/// <summary>
/// Сервис для расчета коэффициента коррекции для задач, чтобы лучше попадать в оценку.
/// </summary>
public class YouTrackProgressReportEstimationCorrector
{
    protected readonly ILogger _logger;

    /// <inheritdoc cref="YouTrackProgressReportEstimationCorrector"/>
    public YouTrackProgressReportEstimationCorrector(ILogger logger)
    {
        logger.AssertNotNull(nameof(logger));

        _logger = logger;
    }

    public async Task<ProgressReportStatistics> BuildReportStatisticsAsync(
        string reportFilePath,
        CancellationToken cancellationToken = default)
    {
        reportFilePath.AssertNotEmpty(nameof(reportFilePath));

        if (!File.Exists(reportFilePath))
            throw new ArgumentException("Report file doesn't exist. Please, check file path", nameof(reportFilePath));

        var personTasksMap = new Dictionary<string, List<TaskStatistics>>();

        var parser = new TextFieldParser(reportFilePath);

        parser.HasFieldsEnclosedInQuotes = true;
        parser.SetDelimiters(",");

        var rowIdx = 0;
        var hasErrors = false;
        while (!parser.EndOfData)
        {
            var cellValues = parser.ReadFields();
            rowIdx++;

            if (rowIdx == 1) continue;

            if (cellValues == null) continue;
            if (cellValues.Length < 6) continue;
            if (GetCellValue(cellValues[1]) == null) continue;

            var personName = GetCellValue(cellValues[0]);
            if (personName == null)
            {
                _logger.LogWarning("Person name is empty at row {RowIdx}", rowIdx);
                hasErrors = true;
                continue;
            }

            if (!personTasksMap.TryGetValue(personName, out var personTasks))
            {
                personTasks = new List<TaskStatistics>();
                personTasksMap[personName] = personTasks;
            }

            var taskCode = GetCellValue(cellValues[1]);
            if (taskCode == null)
            {
                _logger.LogWarning("Task code is empty at row {RowIdx}", rowIdx);
                hasErrors = true;
                continue;
            }

            var taskName = GetCellValue(cellValues[2]);
            if (taskName == null)
            {
                _logger.LogWarning("Task name is empty at row {RowIdx}", rowIdx);
                hasErrors = true;
                continue;
            }

            var estimationRaw = GetCellValue(cellValues[3]);
            if (estimationRaw == null || !int.TryParse(estimationRaw, out var estimationMin) || estimationMin < 0)
            {
                estimationMin = 0;
            }

            var spentTimeRaw = GetCellValue(cellValues[5]);
            if (spentTimeRaw == null || !int.TryParse(spentTimeRaw, out var spentTimeMin) || spentTimeMin < 0)
            {
                spentTimeMin = 0;
            }

            personTasks.Add(new TaskStatistics(
                taskCode,
                taskName,
                new TimeStatistics(
                    estimationMin,
                    spentTimeMin)));
        }

        _logger.LogDebug("Completed reading data from the file");

        if (hasErrors)
        {
            throw new InvalidOperationException("There were errors while parsing report file. Please, see warning.log for details");
        }


        var globalTotalEstimationMin = 0;
        var globalTotalSpentTimeMin = 0;
        var globalAllRatioValues = new List<double>();
        var globalMinRatio = double.MaxValue;
        var globalMaxRatio = double.MinValue;

        var personStatistics = new List<PersonStatistics>();
        foreach (var (personName, personTasks) in personTasksMap)
        {
            if (personTasks.Count == 0)
            {
                _logger.LogWarning("Person \"{PersonName}\" doesn't have any statistics", personName);
                continue;
            }

            var totalEstimationMin = 0;
            var totalSpentTimeMin = 0;

            var allRatioValues = new List<double>(personTasks.Count);
            var minRatio = double.MaxValue;
            var maxRatio = double.MinValue;

            for (var i = 0; i < personTasks.Count; i++)
            {
                var personTask = personTasks[i];

                if (personTask.TimeStatistics.EstimationTimeRatio > maxRatio)
                {
                    maxRatio = personTask.TimeStatistics.EstimationTimeRatio;
                }
                if (personTask.TimeStatistics.EstimationTimeRatio > globalMaxRatio)
                {
                    globalMaxRatio = personTask.TimeStatistics.EstimationTimeRatio;
                }

                if (personTask.TimeStatistics.EstimationTimeRatio < minRatio)
                {
                    minRatio = personTask.TimeStatistics.EstimationTimeRatio;
                }
                if (personTask.TimeStatistics.EstimationTimeRatio < globalMinRatio)
                {
                    globalMinRatio = personTask.TimeStatistics.EstimationTimeRatio;
                }

                allRatioValues.Add(personTask.TimeStatistics.EstimationTimeRatio);
                globalAllRatioValues.Add(personTask.TimeStatistics.EstimationTimeRatio);

                totalEstimationMin += personTask.TimeStatistics.EstimationMin;
                globalTotalEstimationMin += personTask.TimeStatistics.EstimationMin;

                totalSpentTimeMin += personTask.TimeStatistics.SpentTimeMin;
                globalTotalSpentTimeMin += personTask.TimeStatistics.SpentTimeMin;
            }

            var averageRatioPerTasks = allRatioValues.Average();
            var medianRatio = CalculateMedian(allRatioValues);

            var personTotalTimeStatistics = new TimeStatistics(totalEstimationMin, totalSpentTimeMin);
            var personEstimationSpentRatioStatistics = new EstimationSpentRatioStatistics(
                averageRatioPerTasks,
                personTotalTimeStatistics.EstimationTimeRatio,
                minRatio,
                maxRatio,
                medianRatio);

            personStatistics.Add(new PersonStatistics(
                personName,
                personTasks,
                personTotalTimeStatistics,
                personEstimationSpentRatioStatistics));
        }

        var globalAverageRatioPerTasks = globalAllRatioValues.Average();
        var globalMedianRatio = CalculateMedian(globalAllRatioValues);
        var globalTimStatistics = new TimeStatistics(globalTotalEstimationMin, globalTotalSpentTimeMin);

        var globalEstimationSpentRatioStatistics = new EstimationSpentRatioStatistics(
            globalAverageRatioPerTasks,
            globalTimStatistics.EstimationTimeRatio,
            globalMinRatio,
            globalMaxRatio,
            globalMedianRatio);

        return new ProgressReportStatistics(
            personStatistics,
            globalEstimationSpentRatioStatistics,
            personStatistics.Average(x => x.EstimationSpentRatioStatistics.AveragePerTasks));
    }

    private static string? GetCellValue(string? cellValue)
    {
        return cellValue?.Trim().Trim('"').Trim().ToNullIfWhiteSpace();
    }

    private static double CalculateMedian(List<double> values)
    {
        values.Sort();

        var middleIndex = values.Count / 2;
        return values.Count % 2 != 0
            ? values[middleIndex]
            : (values[middleIndex] + values[middleIndex - 1]) / 2;
    }
}
