using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Modules.Tasks;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.TaskMetric
{

    public class TaskMetricCall : BaseMetricCall
    {
        public string Session { get { return TaskSessionMetadata.CurrentSession.SessionIdentifier.ToSessionString(); } }
        public int ParallelTaskCount { get { return TaskSessionMetadata.CurrentSession.ParallelTaskCount; } }

        public TaskMetricCall(string session,
                              int parallelTaskCount,
                              string className,
                              string methodName,
                              MetricTypes metricType,
                              ITimeLine timeLine) :
            base(className,
                 methodName, 
                 MetricTypes.Operation, 
                 timeLine)
        {
        }

        protected override IMetricResult GetResult()
        {
            return new TaskMetricResult(Session, 
                                        ParallelTaskCount,
                                        _metricIdentifier,
                                        _metricType);


        }

        public override void Stop()
        {
            TaskSessionMetadata.CurrentSession.ParallelTaskCount--;
            base.Stop();
        }
    }
}
