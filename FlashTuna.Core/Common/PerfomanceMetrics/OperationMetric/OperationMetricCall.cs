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
        public OperationMetricCall(string moduleName, 
                                   string className,
                                   string methodName,
                                   string tag,
                                   ITimeLine timeLine): 
            base(moduleName,
                 className,
                 methodName, 
                 tag,
                 timeLine)
        {
        }

        protected override IMetricResult GetResult(int status, Guid callId)
        {
            return new OperationMetricResult(status,
                                             _moduleName,
                                             _className,
                                             _methodName,
                                             _tag,
                                             callId);
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
