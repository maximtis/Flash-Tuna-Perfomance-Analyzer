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

        public TaskMetric(ITimeLine timeLine,
                              string methodName = "Undefined Task",
                              string tag = "Task",
                              string moduleName = "Undefned") :
                              base(MetricTypes.Task, timeLine, methodName, tag, moduleName)
        {

        }

        public override IMetricResult GetResult()
        {
            if (!isRunning)
            {
                return new TaskMetricResult(MetricId,
                                            Session,
                                            ParallelTaskCount,
                                            _startTime,
                                            _endTime,
                                            _stopwatch.ElapsedMilliseconds);
            }
            return null;
            
        }
        public override void Start()
        {
            base.Start();
            TaskSessionMetadata.CurrentSession.ParallelTaskCount++;
        }
        public override void Stop()
        {
            base.Stop();
            TaskSessionMetadata.CurrentSession.ParallelTaskCount--;
        }

        public override string ToMetricString()
        {
            return $"{_startTime.ToShortTimeString()} : {_endTime.ToShortTimeString()} ({_stopwatch.ElapsedMilliseconds})";

        }
    }
}
