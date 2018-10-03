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
        long MetricId { get; set; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        long Milliseconds { get; }
        string ToMetricString();
    }
}
