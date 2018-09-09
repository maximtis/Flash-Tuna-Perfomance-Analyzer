using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.Metric.Interfaces
{
    public interface IMetricResult
    {
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        long Milliseconds { get; }
    }
}
