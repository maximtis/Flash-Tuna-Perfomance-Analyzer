﻿using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric
{
    public class OperationMetric : BaseMetric
    {
        public OperationMetric(ITimeLine timeLine,
                              string methodName = "Undefined Operation", 
                              string tag = "Operation",
                              string moduleName = "Undefned") : 
                              base(MetricTypes.Operation, timeLine, methodName, tag, moduleName)
        {
        }

        public override IMetricResult GetResult()
        {
            if (isRunning)
            {
                throw new InvalidOperationException($"Metric of {MethodName} is not finished yet!");
            }
            return new OperationMetricResult(_startTime,
                                             _endTime, 
                                             _stopwatch.ElapsedMilliseconds);
        }

        public override string ToMetricString()
        {
            return $"{_startTime.ToShortTimeString()} : {_endTime.ToShortTimeString()} ({_stopwatch.ElapsedMilliseconds})";
        }
    }
}
