using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using FlashTuna.Core.Common.PerfomanceMetrics.ExceptionMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.TaskMetric;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.Metric.MetricsFactory
{
    public class MetricsFactory
    {
        public static BaseMetric CreateMetric(MetricTypes metricType,
                                              string methodName,
                                              string tag)
        {
            switch (metricType)
            {
                case MetricTypes.Exception:
                    return new ExceptionMetric(FlashTuna.Core.Configuration.FlashTuna.CurrentTimeLine,
                                               methodName,
                                               tag,
                                               FlashTuna.Core.Configuration.FlashTuna.ModuleName);
                case MetricTypes.Operation:
                    return new OperationMetric(FlashTuna.Core.Configuration.FlashTuna.CurrentTimeLine,
                                               methodName,
                                               tag,
                                               FlashTuna.Core.Configuration.FlashTuna.ModuleName);
                case MetricTypes.Task:
                    return new TaskMetric(FlashTuna.Core.Configuration.FlashTuna.CurrentTimeLine,
                                               methodName,
                                               tag,
                                               FlashTuna.Core.Configuration.FlashTuna.ModuleName);

                default:
                    return null;
            }
        }
    }
}
