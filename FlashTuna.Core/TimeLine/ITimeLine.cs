using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using FlashTuna.Core.Storage.DataBase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashTuna.Core.TimeLine
{
    public interface ITimeLine
    {
        Task<IMetricCall> StartMetricAsync(string className, string methodName);
        Task BoundMetric(IMetric metric);
        Task CollectMetricResult(IMetricResult metric);
        void SetStorageProvider(IFlashTunaDbContext dbContext);
    }
}
