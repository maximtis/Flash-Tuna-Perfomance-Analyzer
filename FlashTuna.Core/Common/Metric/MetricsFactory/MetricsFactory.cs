using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.Metric.MetricsFactory
{
    public class MetricsFactory
    {
        public static IMetric CreateMetric(ITimeLine timeLine,string methodName,
                              string tag,
                              string moduleName)
        {
            return new OperationMetric(timeLine,methodName, tag, moduleName);
        }
    }
}
