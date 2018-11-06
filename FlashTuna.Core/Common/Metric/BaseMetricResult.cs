using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlashTuna.Core.Common.Metric
{
    public abstract class BaseMetricResult: IMetricResult
    {

        public BaseMetricResult(string className,
                                string methodName,
                                MetricTypes metricType)
        {
            MetricType = metricType;
            _timePoint = DateTime.Now;
            _metricResultStatus = MetricResultStatus.Started;

            if (StartTimePoint.HasValue)
            {
                TimeSpan span = _timePoint.Subtract(StartTimePoint.Value);
                _milliseconds = span.TotalMilliseconds;
            }
            //Collect Start Data
        }

        private DateTime? _startTimePoint;
        private MetricResultStatus _metricResultStatus;
        private DateTime _timePoint;
        private MetricTypes _metricType;
        private double? _milliseconds;
        [Key]
        public long MetricResultId { get; set; }

        public DateTime TimePoint { get { return _timePoint; } }
        public double? Milliseconds { get { return _milliseconds; } }
        public MetricResultStatus MetricResultStatus { get => _metricResultStatus; }
        public MetricTypes MetricType { get => _metricType; set => _metricType = value; }
        public DateTime? StartTimePoint { get => _startTimePoint; set => _startTimePoint = value; }

        public virtual string ToMetricString()
        {
            return $"Status:{_metricResultStatus.ToString()} - {MetricIdentifier.ClassName}:{MetricIdentifier.MethodName} \n {TimePoint.ToShortTimeString()} ({Milliseconds} ms - {(Milliseconds.HasValue? (Milliseconds / 1000):0)} s)";
        }
    }
}