using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.Modules.Usage;
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
        public async Task<List<string>> GetMetricsByPeriod(int days = 7)
        {
            var from = DateTime.Now.AddDays(-7);
            var to = DateTime.Now;
            List<string> metricResultViewModels = new List<string>();
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var test = await db.OperationMetricResults.ToListAsync();
                var result = await db.OperationMetricResults
                                                    .Where(o =>
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Started && o.TimePoint >= from) ||
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Stopped && o.TimePoint <= to))
                                                    .Select(x => x.MethodName)
                                                    .Distinct()
                                                    .ToListAsync();
                metricResultViewModels = result;
            }
            return metricResultViewModels;
        }

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

        public async Task<List<string>> GetAvailableMetrics()
        {
            List<string> metricResultViewModels = new List<string>();
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var test = await db.OperationMetricResults.ToListAsync();
                var result = await db.OperationMetricResults
                                                    .Select(x => x.MethodName)
                                                    .Distinct()
                                                    .ToListAsync();
                metricResultViewModels = result;
            }
            return metricResultViewModels;
        }


        public async Task<List<MetricResultViewModel>> GetResultsByPeriod(DateTime from, DateTime to, string methodName)
        {
            List<MetricResultViewModel> metricResultViewModels = new List<MetricResultViewModel>();
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var groupedMetricsResults = await db.OperationMetricResults
                                                    .Where(o =>
                                                    o.MethodName == methodName &&
                     ((o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Started && o.TimePoint >= from) ||
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Stopped && o.TimePoint <= to)))
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

        public async Task<List<List<MetricResultViewModel>>> GetResultsRuntime()
        {
            var from = DateTime.Now.AddDays(-1);
            var to = DateTime.Now;
            List<List<MetricResultViewModel>> metricResultViewModelsAll = new List<List<MetricResultViewModel>>();
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var globalGroupedMetricResults = await db.OperationMetricResults.Where(o =>
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Started && o.TimePoint >= from)||
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Stopped && o.TimePoint <= to))
                                                    .GroupBy(x => x.MethodName)
                                                    .ToListAsync();
                foreach(var methodGroupedMetricResult in globalGroupedMetricResults)
                {
                    List<MetricResultViewModel> metricResultViewModels = new List<MetricResultViewModel>();
                    foreach (var groupedMetricResult in methodGroupedMetricResult.GroupBy(x=>x.CallId))
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

                    metricResultViewModelsAll.Add(
                        metricResultViewModels.OrderBy(x => x.StartPoint).ToList()
                        );
                }
                
            }
            return metricResultViewModelsAll;
        }

        public async Task<List<MetricResultViewModel>> GetErrorsResultsRuntime()
        {
            var from = DateTime.Now.AddDays(-1);
            var to = DateTime.Now;
            List<List<MetricResultViewModel>> metricResultViewModelsAll = new List<List<MetricResultViewModel>>();
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var globalGroupedMetricResults = await db.ErrorResults.Where(o =>
                                                     (o.TimePoint >= from) ||
                                                     (o.TimePoint <= to))
                                                    .ToListAsync();

                    List<MetricResultViewModel> metricResultViewModels = new List<MetricResultViewModel>();
                    foreach (var groupedMetricResult in globalGroupedMetricResults)
                    {
                        var metricResult = new MetricResultViewModel()
                        {
                            ClassName = groupedMetricResult.ClassName,
                            StartPoint = groupedMetricResult.TimePoint,
                            EndPoint = groupedMetricResult.TimePoint,
                            MethodName = groupedMetricResult.MethodName,
                            Tag = groupedMetricResult.Tag
                        };
                        metricResultViewModels.Add(metricResult);
                    }
                return metricResultViewModels;
            }
        }

        public async Task<int> AnalyzeProblemsCount()
        {
            var from = DateTime.Now.AddDays(-1);
            var to = DateTime.Now;
            List<List<MetricResultViewModel>> metricResultViewModelsAll = new List<List<MetricResultViewModel>>();
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var globalGroupedMetricResults = await db.OperationMetricResults.Where(o =>
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Started && o.TimePoint >= from) ||
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Stopped && o.TimePoint <= to))
                                                    .GroupBy(x => x.MethodName)
                                                    .ToListAsync();
                foreach (var methodGroupedMetricResult in globalGroupedMetricResults)
                {
                    List<MetricResultViewModel> metricResultViewModels = new List<MetricResultViewModel>();
                    foreach (var groupedMetricResult in methodGroupedMetricResult.GroupBy(x => x.CallId))
                    {
                        var start = groupedMetricResult.OrderBy(x => x.MetricResultStatus).First();
                        var end = groupedMetricResult.OrderBy(x => x.MetricResultStatus).Last();
                        if (start == end)
                            continue;
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

                    metricResultViewModelsAll.Add(
                        metricResultViewModels.OrderBy(x => x.StartPoint).ToList()
                        );
                }

            }
            int problemsCount = 0;
            foreach(var result in metricResultViewModelsAll)
            {
                var timings = result.Select(x=>x.Milliseconds).OrderBy(x => x).ToArray();
                var isProblematic = chechMethodProblem(timings);
                if (isProblematic)
                {
                    problemsCount++;
                }
            }
            return problemsCount;
        }
        public async Task<StatisticReportModel> GetStatisticReport()
        {
            var discoveredMethods = await GetAvailableMetrics();
            var discoveredMethodsCount = discoveredMethods.Count;
            var problemsCount = await AnalyzeProblemsCount();
            var errors = await GetErrorsResultsRuntime();
            var errorsCount = errors.Count;
            var statistiResult = new StatisticReportModel()
            {
                DiscoveredMethods = discoveredMethodsCount,
                ErrorsCount = errorsCount,
                ProblemsCount = problemsCount
            };
            return statistiResult;
        }

        private bool chechMethodProblem(long[] timing)
        {
            long min = 0;
            if (timing.Count() > 1)
            {
                min = timing[0];
            }
            var problemMarker = (long)min * 1.3;
            foreach (var time in timing)
            {
                if (time > problemMarker)
                    return true;
            }
            return false;
        }
    }
}
