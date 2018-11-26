using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.Metric.MetricsFactory
{
    public class MetricsFactory
    {
        public static PerfomanceMetric CreateMetric(string className,
                                              string methodName,
                                              string tag)
        {
            switch (metricType)
            {
                case MetricTypes.Exception:
                    return new ExceptionMetric(className,FlashTuna.Core.Configuration.FlashTuna.CurrentTimeLine,
                                               methodName,
                                               tag,
                                               FlashTuna.Core.Configuration.FlashTuna.ModuleName);
                case MetricTypes.Operation:
                    return new OperationMetric(className, FlashTuna.Core.Configuration.FlashTuna.CurrentTimeLine,
                                               methodName,
                                               tag,
                                               FlashTuna.Core.Configuration.FlashTuna.ModuleName);
                case MetricTypes.Task:
                    return new TaskMetric(className, FlashTuna.Core.Configuration.FlashTuna.CurrentTimeLine,
                                               methodName,
                                               tag,
                                               FlashTuna.Core.Configuration.FlashTuna.ModuleName);

                default:
                    return null;
            }
        }
    }
}
