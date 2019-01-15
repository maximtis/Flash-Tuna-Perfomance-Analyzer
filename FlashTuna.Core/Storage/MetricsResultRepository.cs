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

        public async Task<List<TrackedMethod>> GetTrackedMethods()
        {
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var trackedMethods = await db.TrackedMethods.ToListAsync();
                return trackedMethods;
            }
        }

        public async Task<List<TrackableMethodViewModel>> GetMetricsByPeriod(DateTime from, DateTime to)
        {
            List<TrackableMethodViewModel> metricResultViewModels = new List<TrackableMethodViewModel>();
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var test = await db.OperationMetricResults.ToListAsync();
                var result = await db.OperationMetricResults
                                                    .Where(o => 
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Started && o.TimePoint >= from) ||
                     (o.MetricResultStatus == (int)Common.Metric.MetricResultStatus.Stopped && o.TimePoint <= to))
                                                    .Select(x=> new TrackableMethodViewModel()
                                                    {
                                                        MethodName = x.MethodName,
                                                        ClassName = x.ClassName
                                                    })
                                                    .Distinct()
                                                    .ToListAsync();
                metricResultViewModels = result;
            }
            return metricResultViewModels;
        }

        public async Task<List<TrackableMethodViewModel>> GetAvailableMetrics()
        {
            List<TrackableMethodViewModel> metricResultViewModels = new List<TrackableMethodViewModel>();
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var test = await db.OperationMetricResults.ToListAsync();
                var result = await db.OperationMetricResults
                                                    .Select(x => new TrackableMethodViewModel()
                                                    {
                                                        MethodName = x.MethodName,
                                                        ClassName = x.ClassName
                                                    })
                                                    .Distinct()
                                                    .ToListAsync();
                metricResultViewModels = result;
            }
            return metricResultViewModels;
        }

        public async Task<TrackableMethodViewModel> SetMethodToTrack(TrackableMethodViewModel method)
        {
            var trackedMethods = await GetTrackedMethods();
            if (trackedMethods.Count >= 5 && method.Selected)
            {
                throw new InvalidOperationException("Already selected 5 methods");
            }
            
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                if(await db.TrackedMethods
                    .AnyAsync(x=>x.Name == method.MethodName &&
                                 x.ClassName == method.ClassName) &&
                                                method.Selected)
                {
                    throw new InvalidOperationException($"Method {method.MethodName} already tracked by System.");
                }
                if ((!await db.TrackedMethods
                    .AnyAsync(x => x.Name == method.MethodName &&
                                 x.ClassName == method.ClassName)) &&
                                                (!method.Selected))
                {
                    throw new InvalidOperationException($"Method {method.MethodName} not tracked by System yet.");
                }
                if (method.Selected)
                {
                    await db.TrackedMethods.AddAsync(new TrackedMethod()
                    {
                        ClassName = method.ClassName,
                        Name = method.MethodName
                    });
                    await db.SaveChangesAsync();
                }
                else
                {
                    var trackedMethod = await db.TrackedMethods.FirstAsync(x => x.ClassName == method.ClassName && x.Name == method.MethodName);
                    db.TrackedMethods.Remove(trackedMethod);
                    await db.SaveChangesAsync();
                }
                return new TrackableMethodViewModel()
                {
                    ClassName = method.ClassName,
                    MethodName = method.MethodName,
                    Selected = method.Selected
                };
            }
        }

        public async Task UpdateInterval(IntervalSettingsModel intervalSettingsModel)
        {
            if (intervalSettingsModel.IntervalType >1 && (intervalSettingsModel.IntervalValue> 30 || intervalSettingsModel.IntervalValue < 0))
            {
                throw new InvalidOperationException("You cannot selec more than 30 days to track.");
            }
            if (intervalSettingsModel.IntervalType > 2 && (intervalSettingsModel.IntervalValue > 720 || intervalSettingsModel.IntervalValue < 0))
            {
                throw new InvalidOperationException("You cannot selec more than 30 days to track.");
            }
            if (intervalSettingsModel.IntervalType >  3 && (intervalSettingsModel.IntervalValue > 43200 || intervalSettingsModel.IntervalValue < 0))
            {
                throw new InvalidOperationException("You cannot select more than 30 days to track.");
            }

            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var currentSettings = await db.Settings.FirstAsync();
                currentSettings.Period = intervalSettingsModel.IntervalValue;
                currentSettings.PeriodType = intervalSettingsModel.IntervalType;
                await db.SaveChangesAsync();
            }
        }
        public async Task<IntervalSettingsViewModel> GetTrackingPeriod()
        {
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                if(!await db.Settings.AnyAsync())
                {
                    await db.Settings.AddAsync(new Setting()
                    {
                        ConnectionString = "test",
                        Period = 1,
                        PeriodType = 1
                    });
                    await db.SaveChangesAsync();
                }
                var currentSettings = await db.Settings.FirstAsync();
                var trackingPeriod = new IntervalSettingsViewModel()
                {
                    IntervalType = ParseIntervalType(currentSettings.PeriodType),
                    IntervalValue = currentSettings.Period.ToString()
                };
                return trackingPeriod;
            }
        }

        private string ParseIntervalType(long value)
        {
            switch (value)
            {
                case 1:
                    return "Days";
                case 2:
                    return "Hours";
                case 3:
                    return "Minutes";
            }
            return "";
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
            return metricResultViewModelsAll;
        }

        public async Task<List<ErrorsResultViewModel>> GetErrorsRuntime()
        {
            var from = DateTime.Now.AddDays(-1);
            var to = DateTime.Now;
            List<ErrorsResultViewModel> metricErrorsResultViewModelsAll = new List<ErrorsResultViewModel>();
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                var globalGroupedMetricResults = await db.ErrorResults.Where(o =>
                     (o.TimePoint>from && o.TimePoint<to))
                                                    .GroupBy(x => x.MethodName)
                                                    .ToListAsync();
                foreach (var methodGroupedMetricResult in globalGroupedMetricResults)
                {
                    metricErrorsResultViewModelsAll.Add(new ErrorsResultViewModel()
                    {
                        MethodName = methodGroupedMetricResult.Key,
                        ErrorsCount = methodGroupedMetricResult.Count()
                    });
                }

            }
            return metricErrorsResultViewModelsAll;
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
            var trackingMethods = await GetTrackedMethods();
            var period = await GetTrackingPeriod();
            var convertedMethods = trackingMethods.Select(x => new TrackableMethodViewModel()
            {
                ClassName = x.ClassName,
                MethodName = x.Name,
                Selected = true
            }).ToList();
            var statistiResult = new StatisticReportModel()
            {
                DiscoveredMethods = discoveredMethodsCount,
                ErrorsCount = errorsCount,
                ProblemsCount = problemsCount,
                Methods = convertedMethods,
                TrackingPeriod = $"{period.IntervalValue} {period.IntervalType}",
                UpdateInterval = "5 second"
            };
            
            return statistiResult;
        }
        public async Task ClearData()
        {
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                foreach (var row in db.ErrorResults)
                {
                    db.ErrorResults.Remove(row);
                }
                await db.SaveChangesAsync();

                foreach (var row in db.OperationMetricResults)
                {
                    db.OperationMetricResults.Remove(row);
                }
                await db.SaveChangesAsync();

                foreach (var row in db.TrackedMethods)
                {
                    db.TrackedMethods.Remove(row);
                }
                await db.SaveChangesAsync();

                foreach (var row in db.Settings)
                {
                    db.Settings.Remove(row);
                }
                await db.SaveChangesAsync();

                Setting setting = new Setting()
                {
                    Period = 30,
                    PeriodType = 3,
                };
                db.Settings.Add(setting);
                await db.SaveChangesAsync();
            }
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
