using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Modules.Tasks;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.TaskMetric
{
    public class TaskMetric : BaseMetric 
    {
        public string Session { get { return SessionIdentifier.ToSessionString(); } }
        public int ParallelTaskCount { get { return TaskSessionMetadata.CurrentSession.ParallelTaskCount; } }

        public TaskMetric(string className,
                              ITimeLine timeLine,
                              string methodName = "Undefined Task",
                              string tag = "Task",
                              string moduleName = "Undefned") :
                              base(className,MetricTypes.Task, timeLine, methodName, tag, moduleName)
        {

        }

        public override IMetricCall Start()
        {
            TaskSessionMetadata.CurrentSession.ParallelTaskCount++;
            return new TaskMetricCall(Session, ParallelTaskCount, GetIdentidier(), MetricType, BoundedTimeLine);
        }

    }
}
