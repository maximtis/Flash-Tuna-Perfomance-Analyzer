using FlashTuna.Core.Attributes.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlashTuna.Core.Common.Metric.Interfaces
{
    public interface IMetricResult
    {
        MetricResultStatus MetricResultStatus { get; }
        DateTime TimePoint { get; set; }
        double? Milliseconds { get; set; }
        string ClassName { get; set; }
        string MethodName { get; set; }
        string Tag { get; set; }
        string ModuleName { get; set; }
        string ToMetricString();
    }
}
