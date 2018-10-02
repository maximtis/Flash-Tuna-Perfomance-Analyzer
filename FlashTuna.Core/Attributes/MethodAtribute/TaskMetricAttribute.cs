using FlashTuna.Core.Attributes.Common;
using System;
using System.Runtime.CompilerServices;

namespace FlashTuna.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TaskMetricAttribute : PerfomanceMetricAttribute
    {
        public TaskMetricAttribute(string className,[CallerMemberName] string methodName = null,string tag = "TaskMetric") :
            base(className,Core.Common.PerfomanceMetrics.MetricTypes.Task, methodName, tag)
        {
        }
    }
}
