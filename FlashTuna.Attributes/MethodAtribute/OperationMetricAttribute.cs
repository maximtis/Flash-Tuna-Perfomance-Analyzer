using FlashTuna.Attributes.Common;
using System;
using System.Runtime.CompilerServices;

namespace FlashTuna.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OperationMetricAttribute : PerfomanceMetricAttribute
    {

        public OperationMetricAttribute([CallerMemberName] string methodName = null, string tag = "OperationMetric") :
            base(Core.Common.PerfomanceMetrics.MetricTypes.Task, methodName, tag)
        {
        }
    }
}
