using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Modules.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.TaskMetric
{

    public class TaskMetric : BaseMetric
    {
        public string ExceptionName { get; set; }
        public string ExceptioType { get; set; }

        public TaskMetric(ITimeLine timeLine,
                              string methodName = "Undefined Task",
                              string tag = "Task",
                              string moduleName = "Undefned") :
                              base(MetricTypes.Task, timeLine, methodName, tag, moduleName)
        {
        }

        public override IMetricResult GetResult()
        {
            throw new NotImplementedException();
        }

        public override string ToMetricString()
        {
            throw new NotImplementedException();
        }
    }
}
