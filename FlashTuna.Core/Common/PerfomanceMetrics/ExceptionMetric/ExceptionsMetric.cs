using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.ExceptionMetric
{
    public class ExceptionMetric : BaseMetric
    {
        public string ExceptionName { get; set; }
        public string ExceptionType { get; set; }

        public ExceptionMetric(string exceptionName,
                               string exceptionType,
                               string className,
                               ITimeLine timeLine, 
                               string methodName = "Undefined Exception",
                               string tag = "Operation",
                               string moduleName = "Undefned") :
                               base(className, MetricTypes.Exception, timeLine, methodName, tag, moduleName)
        {
            ExceptionName = exceptionName;
            ExceptionType = exceptionType;
        }

        
        public override IMetricCall Start()
        {
            return new ExceptionMetricCall(ExceptionName, ExceptionType, GetIdentidier(), MetricType, BoundedTimeLine);
        }
    }
}
