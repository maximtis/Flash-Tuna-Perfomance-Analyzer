using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.Modules.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.ExceptionMetric
{
    public class ExceptionMetric : BaseMetric
    {
        public string ExceptionName { get; set; }
        public string ExceptioType { get; set; }

        public ExceptionMetric(ITimeLine timeLine, 
                              string methodName = "Undefined Exception",
                              string tag = "Operation",
                              string moduleName = "Undefned") :
                              base(MetricTypes.Exception, timeLine, methodName, tag, moduleName)
        {
        }

        public override IMetricResult GetResult()
        {
            if (!isUsed)
            {
                throw new InvalidOperationException($"Metric of {MethodName} method not used yet!");
            }
            if (!isRunning)
            {
                throw new InvalidOperationException($"Metric of {MethodName} is not finished yet!");
            }
            return new ExceptionMetricResult(_startTime,
                                             _endTime,
                                             _stopwatch.ElapsedMilliseconds);
        }


        public override string ToMetricString()
        {
            return $"{_startTime.ToShortTimeString()} : {_endTime.ToShortTimeString()} ({_stopwatch.ElapsedMilliseconds})";
        }
    }
}
