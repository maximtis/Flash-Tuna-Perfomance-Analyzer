using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric
{
    public class ExceptionMetricResult : BaseMetricResult
    {

        string _exceptionName, _exceptionType;

        public ExceptionMetricResult(string exceptionName, string exceptioType, MetricKey identifier, DateTime startTime,DateTime endTime, long milliseconds):
            base(identifier, startTime, endTime, milliseconds)
        {
            _exceptionName = exceptionName;
            _exceptionType = exceptioType;
        }

        public string ExceptionName { get { return _exceptionName; } }
        public string ExceptioType { get { return _exceptionType; } }

        public override string ToMetricString()
        {
            return base.ToMetricString() + $"\n {ExceptionName} : {ExceptioType}";
        }

    }
}
