using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Data.Storage
{
    public interface IMetricStorage
    {
        Task<IMetric> AddMetric(IMetric metric);
        Task<IMetric> EditMetric(string methodName, IMetric metric);
        Task<IMetric> RemoveMetric(IMetric metric);

        Task AddMetricResult(IMetricResult result);
        Task<IEnumerable<IMetric>> GetMetrics();
    }
}
