using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.Common.Metric
{
    public abstract class BaseMetric : IMetric
    {
        public BaseMetric(MetricTypes metricType,
                          string methodName = "Undefined",
                          string tag = "",
                          string moduleName = "Undefned")
        {
            MethodName = methodName;
            Tag = tag;
            ModuleName = moduleName;
            MetricType = metricType;
        }

        public string MethodName { get; set; }
        public string Tag { get; set; }
        public string ModuleName { get; set; }
        public bool isRunning { get; set; }
        public bool isUsed { get ; set ; }
        public MetricTypes MetricType { get; }


        protected Stopwatch _stopwatch;
        protected DateTime _startTime;
        protected DateTime _endTime;

        public virtual void Start()
        {
            isRunning = true;
            _startTime = DateTime.Now;
            _stopwatch.Start();
        }

        public virtual void Stop()
        {
            isUsed = true;
            isRunning = false;
            _endTime = DateTime.Now;
            _stopwatch.Stop();
        }

        public abstract string ToMetricString();
        public abstract IMetricResult GetResult();

    }
}
