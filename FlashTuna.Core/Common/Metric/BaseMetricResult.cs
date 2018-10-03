using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.Metric
{
    public abstract class BaseMetricResult: IMetricResult
    {
        DateTime _startTime, _endTime;
        long _milliseconds;
        MetricKey _metricIdentifier;

        public BaseMetricResult(MetricKey identifier, DateTime startTime, DateTime endTime, long milliseconds)
        {
            _metricIdentifier = identifier;
            _startTime = startTime;
            _endTime = endTime;
            _milliseconds = milliseconds;
        }

        public long MetricResultId { get; set; }
        public long MetricId { get; set; }
        public DateTime StartTime { get { return _startTime; } }
        public DateTime EndTime { get { return _endTime; } }
        public long Milliseconds { get { return _milliseconds; } }

        public virtual string ToMetricString()
        {
            return $"{_metricIdentifier.ClassName}:{_metricIdentifier.MethodName} \n {StartTime.ToShortTimeString()} : {EndTime.ToShortTimeString()} ({Milliseconds})";
        }
    }
}