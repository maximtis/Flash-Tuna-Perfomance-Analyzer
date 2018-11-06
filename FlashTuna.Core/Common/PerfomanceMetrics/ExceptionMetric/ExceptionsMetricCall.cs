using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.ExceptionMetric
{
    public class ExceptionMetricCall : BaseMetricCall
    {
        public string ExceptionName { get; set; }
        public string ExceptioType { get; set; }

        public ExceptionMetricCall(string exceptionName,
                                   string exceptionType ,
                                   MetricKey metricIdentifier,
                                   MetricTypes metricType,
                                   ITimeLine timeLine):base(metricIdentifier,MetricTypes.Exception,timeLine)
        {
            ExceptionName = exceptionName;
            ExceptioType = exceptionType;
        }

        protected override IMetricResult GetResult()
        {
                return new ExceptionMetricResult(ExceptionName,
                                                 ExceptioType,
                                                 _metricIdentifier,
                                                 _metricType);
        }
        public override void Stop()
        {
            base.Stop();
        }
    }
}
