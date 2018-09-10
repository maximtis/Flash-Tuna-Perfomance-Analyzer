using FlashTuna.Core.Common.Metric.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Data.Storage
{
    public interface IMetricStorage
    {
        Task<IMetric> AddMetric(IMetric metric);
        Task<IMetric> EditMetric(long id, IMetric metric);
        Task<IMetric> RemoveMetric(IMetric metric);

        Task AddMetricResult(IMetricResult result);
        Task<IEnumerable<IMetric>> GetMetrics();
        Task<IEnumerable<IMetric>> GetMetrics(DateTime from, DateTime to);
        Task<IEnumerable<IMetric>> GetMetrics(string tag, DateTime from, DateTime to);
        Task<IEnumerable<IMetric>> GetMetrics(MetricTypes metricType,  string tag, DateTime from, DateTime to);
        Task<IEnumerable<IMetric>> GetMetrics(DateTime from, DateTime to, string methodName);
        Task<IEnumerable<IMetric>> GetMetrics(DateTime from, DateTime to, string methodName,string moduleName);
        Task<IEnumerable<IMetric>> GetMetrics(DateTime from, DateTime to, MetricTypes metricType, string methodName);
        Task<IEnumerable<IMetric>> GetMetrics(DateTime from, DateTime to, MetricTypes metricType, string methodName, string modeuleName);
    }
}
