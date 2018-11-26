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
        public OperationMetricResult(string className,
                                     string methodName) :
            base(className,
                 methodName,
                 metricType)
        {

        }

        public override string ToMetricString()
        {
            return base.ToMetricString();
        }
    }
}
