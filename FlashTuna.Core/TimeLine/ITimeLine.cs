using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Storage.DataBase;
using System;
using System.Threading.Tasks;

namespace FlashTuna.Core.TimeLine
{
    public interface ITimeLine
    {
        Task<IMetricCall> StartMetricAsync(string className, string methodName);
        Task CollectException(IErrorResult errorResult);
        Task BoundMetric(IMetric metric);
        Task CollectMetricResult(IMetricResult metric);
    }
}
