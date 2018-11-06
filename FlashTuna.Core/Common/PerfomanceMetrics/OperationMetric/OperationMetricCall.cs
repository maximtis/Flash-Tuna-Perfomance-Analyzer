using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric
{
    public class OperationMetricCall : BaseMetricCall
    {
        public OperationMetricCall(MetricKey metricIdentifier,
                              MetricTypes metricType,
                              ITimeLine timeLine) : 
                              base(metricIdentifier, MetricTypes.Operation, timeLine)
        {
        }

        protected override IMetricResult GetResult()
        {
            return new OperationMetricResult(_metricIdentifier,
                                             _metricType);
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
