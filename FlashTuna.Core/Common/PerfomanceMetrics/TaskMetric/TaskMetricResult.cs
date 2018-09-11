using FlashTuna.Core.Common.Metric.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.TaskMetric
{

    class TaskMetricResult : IMetricResult
    {
        DateTime _startTime, _endTime;
        long _milliseconds;

        public TaskMetricResult(DateTime startTime, DateTime endTime, long milliseconds)
        {
            _startTime = startTime;
            _endTime = endTime;
            _milliseconds = milliseconds;
        }

        public long MetricResultId { get; set; }
        public long MetricId { get; set; }
        public DateTime StartTime { get { return _startTime; } }
        public DateTime EndTime { get { return _endTime; } }
        public long Milliseconds { get { return _milliseconds; } }
    }
}
