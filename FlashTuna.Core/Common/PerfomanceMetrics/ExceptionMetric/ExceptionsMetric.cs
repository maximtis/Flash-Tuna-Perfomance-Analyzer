using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.PerfomanceMetrics.ExceptionMetric
{
    public class ExceptionsMetric : BaseMetric
    {
        public string ExceptionName { get; set; }
        public string ExceptioType { get; set; }

        public ExceptionsMetric(string methodName = "Undefined Exception",
                              string tag = "Operation",
                              string moduleName = "Undefned") :
                              base(MetricTypes.Exception, methodName, tag, moduleName)
        {
        }

        public override IMetricResult GetResult()
        {
            throw new NotImplementedException();
        }

        public override string ToMetricString()
        {
            throw new NotImplementedException();
        }
    }
}
