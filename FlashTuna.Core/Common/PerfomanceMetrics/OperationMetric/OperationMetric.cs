using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric
{
    public class OperationMetric : BaseMetric
    {
        public OperationMetric(string className,
                              ITimeLine timeLine,
                              string methodName = "Undefined Operation", 
                              string tag = "Operation",
                              string moduleName = "Undefned") : 
                              base(className, timeLine, methodName, tag, moduleName)
        {
        }

        public async override Task<IMetricCall> StartAsync()
        {
            return new OperationMetricCall(ModuleName,ClassName,MethodName,Tag, BoundedTimeLine);
        }
    }
}
