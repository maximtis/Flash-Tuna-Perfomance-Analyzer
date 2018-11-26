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
        public OperationMetricResult(string moduleName,
                                     string className,
                                     string methodName,
                                     string tag) :
            base(moduleName,
                 className,
                 methodName,
                 tag)
        {

        }

        public override string ToMetricString()
        {
            return base.ToMetricString();
        }
    }
}
