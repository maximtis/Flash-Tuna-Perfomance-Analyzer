using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric;
using FlashTuna.Core.Modules.Runtime;
using FlashTuna.Core.Storage.DataBase;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.TimeLine
{ 
    internal sealed class TimeLine : ITimeLine
    {
        private FlashTunaDbContext db = null;
        private HashSet<IMetric> _metrics = new HashSet<IMetric>();
        private List<IMetricResult> _metricsResults = new List<IMetricResult>();
        public void SetStorageProvider(IFlashTunaDbContext storageProvider){
             //db = storageProvider as FlashTunaDbContext;
        }

        public TimeLine()
        {
        }

        public async Task BoundMetric(IMetric metric)
        {
            _metrics.Add(metric as OperationMetric);
            await Task.FromResult(0);
        }

        public async Task CollectMetricResult(IMetricResult metricResult)
        {
            using(FlashTunaDbContext db = new FlashTunaDbContext())
            {
                await db.OperationMetricResults.AddAsync(metricResult as OperationMetricResult);
                await db.SaveChangesAsync();
            }
        }

        public async Task<IMetricCall> StartMetricAsync(string className, string methodName)
        {
            var targetMetric = _metrics.SingleOrDefault(x => x.ClassName == className && x.MethodName == methodName);
            return await targetMetric.StartAsync();
        }
    }
}
