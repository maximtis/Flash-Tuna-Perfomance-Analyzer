using FlashTuna.Attributes.Common;
using System;
using System.Runtime.CompilerServices;

namespace FlashTuna.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TaskMetricAttribute : PerfomanceMetricAttribute
    {
        public TaskMetricAttribute([CallerMemberName] string methodName = null,string tag = "TaskMetric") :
            base(Core.Common.PerfomanceMetrics.MetricTypes.Task, methodName, tag)
        {
        }
    }
}
