using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric;
using FlashTuna.Core.Modules.Tasks;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.Common.Metric
{
    public abstract class BaseMetric : IMetric
    {
        public BaseMetric(string className,
                          ITimeLine timeLine,
                          string methodName = "Undefined",
                          string tag = "",
                          string moduleName = "Undefned")
        {
            BoundedTimeLine = timeLine;
            BoundedTimeLine.BoundMetric(this);
            Tag = tag;
            MethodName = methodName;
            ClassName = className;
            ModuleName = moduleName;
            SessionIdentifier = TaskSessionMetadata.CurrentSession.SessionIdentifier;
        }

        public TaskSessionIdentifier SessionIdentifier { get;set; }
        public ITimeLine BoundedTimeLine { get; }

        public string Session { get { return SessionIdentifier.ToSessionString(); } }
        public int ParallelTaskCount { get { return TaskSessionMetadata.CurrentSession.ParallelTaskCount; } }

        public string Tag { get; set; }
        public string ModuleName { get; set; }
        [Key]
        public long MetricId { get; set ; }
        public string ClassName { get; set ; }
        public string MethodName { get; set; }

        public abstract Task<IMetricCall> StartAsync();
    }
}
