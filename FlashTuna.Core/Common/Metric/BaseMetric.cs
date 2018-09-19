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
        public BaseMetric(MetricTypes metricType,
                          ITimeLine timeLine,
                          string methodName = "Undefined",
                          string tag = "",
                          string moduleName = "Undefned")
        {
            BoundedTimeLine = timeLine;
            MethodName = methodName;
            Tag = tag;
            ModuleName = moduleName;
            MetricType = metricType;
            SessionIdentifier = TaskSessionMetadata.CurrentSession.SessionIdentifier;
            _stopwatch = new Stopwatch();
        }


        public TaskSessionIdentifier SessionIdentifier {get;set;}
        public bool isRunning { get; set; }
        public MetricTypes MetricType { get; }
        public ITimeLine BoundedTimeLine { get; }
        public string MethodName { get; set; }
        public string Tag { get; set; }
        public string ModuleName { get; set; }
        [Key]
        public long MetricId { get; set ; }

        protected Stopwatch _stopwatch;
        protected DateTime _startTime;
        protected DateTime _endTime;

        public virtual void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
                _endTime = DateTime.Now;
                _stopwatch.Stop();

                //Collect Metric Result
                BoundedTimeLine.CollectMetricResult(GetResult());
            }
        }

        public abstract string ToMetricString();
        public abstract IMetricResult GetResult();

        public virtual void Start()
        {
            if (!isRunning)
            {
                BoundedTimeLine.BoundMetric(this);
                isRunning = true;
                _startTime = DateTime.Now;
                _stopwatch.Start();
            }
        }
    }
}
