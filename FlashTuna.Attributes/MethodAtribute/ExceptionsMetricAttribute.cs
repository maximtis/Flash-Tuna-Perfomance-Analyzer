using FlashTuna.Attributes.Common;
using System;
using System.Runtime.CompilerServices;

namespace FlashTuna.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExceptionsMetricAttribute : PerfomanceMetricAttribute
    {
        public ExceptionsMetricAttribute([CallerMemberName] string methodName = null, string tag = "ExceptionsMetric") :
           base(Core.Common.PerfomanceMetrics.MetricTypes.Task, methodName, tag)
        {
        }
    }
}
