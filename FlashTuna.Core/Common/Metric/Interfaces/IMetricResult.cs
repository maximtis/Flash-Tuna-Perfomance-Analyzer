using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.PerfomanceMetrics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlashTuna.Core.Common.Metric.Interfaces
{
    public interface IMetricResult
    {
        [Key]
        long MetricResultId { get; set; }
        MetricResultStatus MetricResultStatus { get; }
        DateTime TimePoint { get; }
        double? Milliseconds { get; }
        MetricTypes MetricType { get; }
        MetricKey MetricIdentifier { get; }
        string ToMetricString();
    }
}
