using FlashTuna.Core.Attributes.Common;
using System;
using System.Runtime.CompilerServices;

namespace FlashTuna.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExceptionsMetricAttribute : PerfomanceMetricAttribute
    {
        public ExceptionsMetricAttribute(string className,[CallerMemberName] string methodName = null, string tag = "ExceptionsMetric") :
            base(className,Core.Common.PerfomanceMetrics.MetricTypes.Task, methodName, tag)
        {
        }
    }
}
