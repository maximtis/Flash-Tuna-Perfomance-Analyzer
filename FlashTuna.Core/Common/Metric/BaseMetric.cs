using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
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
                          MetricTypes metricType,
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
            MetricType = metricType;
            SessionIdentifier = TaskSessionMetadata.CurrentSession.SessionIdentifier;
        }

        protected MetricKey GetIdentidier()
        {
            return new MetricKey()
            {
                MetricCallId = new Random().Next(1000000, 9999999),
                ClassName = ClassName,
                MethodName = MethodName
            };
        }
        public TaskSessionIdentifier SessionIdentifier {get;set;}
        public MetricTypes MetricType { get; }
        public ITimeLine BoundedTimeLine { get; }

        public string Tag { get; set; }
        public string ModuleName { get; set; }
        [Key]
        public long MetricId { get; set ; }
        public string ClassName { get; set ; }
        public string MethodName { get; private set; }

        public abstract IMetricCall Start();
    }
}
