using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric
{
    public class MetricResultViewModel
    {
        public string Tag { get; set; }
        public string ModuleName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public long Milliseconds { get; set; }
        public DateTime StartPoint { get; set; }
        public DateTime EndPoint { get; set; }
        public long Id { get; set; }
    } 

    public class OperationMetricResult : BaseMetricResult
    {
        public OperationMetricResult(string moduleName,
                                     string className,
                                     string methodName,
                                     string tag,
                                     Guid callId,
                                     MetricResultStatus status) :
            base(moduleName,
                 className,
                 methodName,
                 tag,
                 callId,
                 status)
        {

        }

        public override string ToMetricString()
        {
            return base.ToMetricString();
        }
    }
}
