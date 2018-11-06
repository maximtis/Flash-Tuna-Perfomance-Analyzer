using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.TaskMetric
{

    public class TaskMetricResult : BaseMetricResult
    {
        public string Session { get; set; }
        public int ParallelTaskCount { get; set; }

        public TaskMetricResult(string session,
                                int parralelTaskCount,
                                MetricKey metricIdentifier,
                                MetricTypes metricType) :
                                base(metricIdentifier, metricType)
        {
            Session = session;
            ParallelTaskCount = parralelTaskCount;
        }
       
        public override string ToMetricString()
        {
            return base.ToMetricString() + $"\n {Session} : {ParallelTaskCount}";
        }
    }
}
