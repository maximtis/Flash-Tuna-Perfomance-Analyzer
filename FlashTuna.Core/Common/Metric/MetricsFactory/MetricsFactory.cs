using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.Metric.MetricsFactory
{
    public class MetricsFactory
    {
        public static OperationMetric CreateMetric(string className,
                                              string methodName,
                                              string tag)
        {

                    return new OperationMetric(className, 
                                               FlashTuna.Core.Configuration.FlashTuna.CurrentTimeLine,
                                               methodName,
                                               tag,
                                               FlashTuna.Core.Configuration.FlashTuna.ModuleName);
               
        }
    }
}
