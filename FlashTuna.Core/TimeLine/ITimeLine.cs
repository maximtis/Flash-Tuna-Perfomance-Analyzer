using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlashTuna.Core.TimeLine
{
    public interface ITimeLine
    {
        Task StartMetric(string className, string methodName);
        Task StopMetric(string className, string methodName);
        Task BoundMetric(IMetric metric);
        Task CollectMetricResult(IMetricResult metric);
        Task<IEnumerable<IMetricResult>> ExtractMetricResult();
    }
}
