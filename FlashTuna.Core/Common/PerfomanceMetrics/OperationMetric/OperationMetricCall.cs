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
        public OperationMetricCall(string className,
                                   string methodName,
                                   ITimeLine timeLine) : 
            base(className,
                 methodName,
                 timeLine)
        {
        }

        protected override IMetricResult GetResult()
        {
            return new OperationMetricResult(_className,
                                             _methodName);
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
