﻿using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.Modules.TimeLine
{
    public interface ITimeLine
    {
        Task BoundMetric(IMetric metric);
        Task CollectMetricResult(IMetricResult metric);
        Task<IEnumerable<IMetric>> GetBoundMetrics();
        Task<IEnumerable<IMetricResult>> GetMetricResult(string tag);
        Task<IEnumerable<IMetricResult>> GetMetricResult(MetricTypes metricsType);
        Task<IEnumerable<IMetricResult>> GetMetricResult(MetricTypes metricsType, string methodName);
        Task<IEnumerable<IMetricResult>> GetMetricResult(MetricTypes metricsType, string methodName,string moduleName);
    }
}
