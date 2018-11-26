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
                                string methodName)
        {
            TimePoint = DateTime.Now;
            MetricResultStatus = MetricResultStatus.Started;

            if (StartTimePoint.HasValue)
            {
                TimeSpan span = TimePoint.Subtract(StartTimePoint.Value);
                Milliseconds = span.TotalMilliseconds;
            }
            //Collect Start Data
        }
        public string Tag { get; set; }
        public string ModuleName { get; set; }
        [Key]
        public long MetricId { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }

        [Key]
        public long Id { get; set; }

        public DateTime TimePoint { get; set; }
        public double? Milliseconds { get; set; }
        public MetricResultStatus MetricResultStatus { get; set; }
        public DateTime? StartTimePoint { get; set; }

        public virtual string ToMetricString()
        {
            return $"Status:{MetricResultStatus.ToString()} - {ClassName}:{MethodName} \n {TimePoint.ToShortTimeString()} ({Milliseconds} ms - {(Milliseconds.HasValue? (Milliseconds / 1000):0)} s)";
        }
    }
}