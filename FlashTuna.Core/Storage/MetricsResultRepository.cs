using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.Storage.DataBase;
using FlashTuna.Core.TimeLine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.Storage
{
    public class MetricsResultRepository
    {
        public async Task<List<string>> GetMetricsByPeriod(DateTime from, DateTime to)
        {
            from = new DateTime(2000, 1, 1);
            to = new DateTime(2019, 1, 1);
            List<string> metricResultViewModels = new List<string>();
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var test = await db.OperationMetricResults.ToListAsync();
                var result = await db.OperationMetricResults
                                                    .Where(o => 
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Started && o.TimePoint >= from) ||
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Stopped && o.TimePoint <= to))
                                                    .Select(x=>x.MethodName)
                                                    .Distinct()
                                                    .ToListAsync();
                metricResultViewModels = result;
            }
            return metricResultViewModels;
        }

        public async Task<List<MetricResultViewModel>> GetResultsByPeriod(DateTime from, DateTime to, string methodName)
        {
            from = new DateTime(2000, 1, 1);
            to = new DateTime(2019, 1, 1);
            List<MetricResultViewModel> metricResultViewModels = new List<MetricResultViewModel>();
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var groupedMetricsResults = await db.OperationMetricResults
                                                    .Where(o =>
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Started && o.TimePoint >= from) ||
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Stopped && o.TimePoint <= to))
                                                    .GroupBy(x => x.CallId)
                                                    .ToListAsync();

                foreach (var groupedMetricResult in groupedMetricsResults)
                {
                    var start = groupedMetricResult.OrderBy(x => x.MetricResultStatus).First();
                    var end = groupedMetricResult.OrderBy(x => x.MetricResultStatus).Last();
                    var metricResult = new MetricResultViewModel()
                    {
                        ClassName = start.ClassName,
                        StartPoint = start.TimePoint,
                        EndPoint = end.TimePoint,
                        Id = start.CallId,
                        MethodName = start.MethodName,
                        Milliseconds = (end.TimePoint.TimeOfDay - start.TimePoint.TimeOfDay).Milliseconds,
                        ModuleName = start.ModuleName,
                        Tag = start.Tag
                    };
                    metricResultViewModels.Add(metricResult);
                }
            }
            return metricResultViewModels.OrderBy(x=>x.StartPoint).ToList();
        }
    }
}
