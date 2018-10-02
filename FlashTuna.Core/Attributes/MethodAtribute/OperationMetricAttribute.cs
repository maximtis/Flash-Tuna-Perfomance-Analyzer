using FlashTuna.Core.Attributes.Common;
using System;
using System.Runtime.CompilerServices;

namespace FlashTuna.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OperationMetricAttribute : PerfomanceMetricAttribute
    {
        public OperationMetricAttribute(string className,[CallerMemberName] string methodName = null, string tag = "OperationMetric") :
            base(className,Core.Common.PerfomanceMetrics.MetricTypes.Task, methodName, tag)
        {
        }
    }
}
