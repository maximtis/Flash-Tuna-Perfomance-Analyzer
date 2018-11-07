using FlashTuna.Core.Common.PerfomanceMetrics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.Common.Metric.Interfaces
{
    public interface IMetric
    {
        long MetricId { get; set; }
        string ClassName { get; set; }
        string Tag { get; set; }
        string ModuleName { get; set; }
        MetricTypes MetricType { get; set; }
        IMetricCall Start();
    }
}
