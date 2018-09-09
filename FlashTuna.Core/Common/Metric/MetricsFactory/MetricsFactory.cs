using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.Metric.MetricsFactory
{
    public class MetricsFactory
    {
        public static IMetric CreateMetric(string methodName,
                              string tag,
                              string moduleName)
        {
            return new OperationMetric(methodName, tag, moduleName);
        }
    }
}
