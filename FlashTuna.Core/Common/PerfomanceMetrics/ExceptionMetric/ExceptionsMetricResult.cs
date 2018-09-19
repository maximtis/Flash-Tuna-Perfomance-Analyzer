using FlashTuna.Core.Common.Metric.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric
{
    public class ExceptionMetricResult : IMetricResult
    {
        DateTime _startTime, _endTime;
        long _milliseconds;
        string _exceptionName, _exceptioType;

        public ExceptionMetricResult(string exceptionName, string exceptioType, DateTime startTime,DateTime endTime, long milliseconds)
        {
            _startTime = startTime;
            _endTime = endTime;
            _milliseconds = milliseconds;
            _exceptionName = exceptionName;
            _exceptioType = exceptioType;
        }
        public long MetricResultId { get; set; }
        public long MetricId { get; set; }
        public DateTime StartTime { get { return _startTime; } }
        public DateTime EndTime { get { return _endTime; } }
        public long Milliseconds { get { return _milliseconds; } }
        public string ExceptionName { get { return _exceptionName; } }
        public string ExceptioType { get { return _exceptioType; } }

    }
}
