using FlashTuna.Core.Common.Metric.MetricsFactory;
using FlashTuna.Core.Common.PerfomanceMetrics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Attributes.Common
{
    public class PerfomanceMetricAttribute : Attribute
    {
        public PerfomanceMetricAttribute(MetricTypes metricType,string methodName,string tag) : base()
        {
            MetricsFactory.CreateMetric(metricType, methodName, tag);
        }
    }
}
