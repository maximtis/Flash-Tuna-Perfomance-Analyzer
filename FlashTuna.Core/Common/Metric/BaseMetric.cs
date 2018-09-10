﻿using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using FlashTuna.Core.Modules.TimeLine;
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
            _stopwatch = new Stopwatch();
        }
        public long MetricId { get; set; }
        public string MethodName { get; set; }
        public string Tag { get; set; }
        public string ModuleName { get; set; }
        public bool isRunning { get; set; }
        public bool isUsed { get ; set ; }
        public MetricTypes MetricType { get; }
        public ITimeLine BoundedTimeLine { get; }

        protected Stopwatch _stopwatch;
        protected DateTime _startTime;
        protected DateTime _endTime;

        public virtual void Stop()
        {
            if (isRunning)
            {
                isUsed = true;
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
            BoundedTimeLine.BoundMetric(this);
            isRunning = true;
            _startTime = DateTime.Now;
            _stopwatch.Start();
        }
    }
}
