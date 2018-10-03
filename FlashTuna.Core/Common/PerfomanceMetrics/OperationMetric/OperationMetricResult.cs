using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric
{
    public class OperationMetricResult : BaseMetricResult
    {
        DateTime _startTime, _endTime;
        long _milliseconds;

        public OperationMetricResult(MetricKey identifier,DateTime startTime,DateTime endTime, long milliseconds):
            base(identifier,  startTime,  endTime,  milliseconds)
        {

        }
    }
}
