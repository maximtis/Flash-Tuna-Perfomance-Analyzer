using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.Metric.Interfaces
{
    public interface IMetricResult
    {
        long MetricResultId { get; set; }
        long MetricId { get; set; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        long Milliseconds { get; }
    }
}
