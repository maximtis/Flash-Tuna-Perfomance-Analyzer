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

        DateTime _startTime, _endTime;
        long _milliseconds;

        public TaskMetricResult(long metricId,string session,int taskCount,MetricKey identifier, DateTime startTime, DateTime endTime, long milliseconds):
            base(identifier, startTime, endTime, milliseconds)
        {
            Session = session;
            ParallelTaskCount = taskCount;
        }

        public override string ToMetricString()
        {
            return base.ToMetricString() + $"\n {Session} : {ParallelTaskCount}";
        }

    }
}
