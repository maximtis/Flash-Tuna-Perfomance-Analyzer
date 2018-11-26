using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using FlashTuna.Core.Common.PerfomanceMetrics.ExceptionMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.TaskMetric;
using FlashTuna.Core.Storage.DataBase;
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

        public void SetStorageProvider(FlashTunaDbContext storageProvider){
            db = storageProvider;
        }

        public TimeLine()
        {
        }

        // TODO:
        // - Check if metric exists, if no add to DataBase
        public async Task BoundMetric(IMetric metric)
        {
            switch (metric.MetricType)
            {
                case MetricTypes.Operation:
                    await db.OperationMetrics.AddAsync(metric as OperationMetric);
                    break;
                case MetricTypes.Task:
                    await db.TaskMetrics.AddAsync(metric as TaskMetric);
                    break;
                case MetricTypes.Exception:
                    await db.ExceptionMetrics.AddAsync(metric as ExceptionMetric);
                    break;
            }
            await db.SaveChangesAsync();
        }

        public async Task CollectMetricResult(IMetricResult metricResult)
        {
            _metricsResults.Add(metricResult);
        }

        public async Task<IEnumerable<IMetricResult>> ExtractMetricResult()
        {
            return _metricsResults?.OrderBy(x => x?.TimePoint); 
        }

        // TODO:
        // need to handle multiple metrics on demand, update their info in the DataBase in real time.
        // Sync code and database at the start.
        public async Task<IMetricCall> StartMetricAsync(string className, string methodName)
        {
            List<PerfomanceMetric> foundMetrics = new List<PerfomanceMetric>();
            foundMetrics.Add(await db.OperationMetrics.SingleOrDefaultAsync(x => x.ClassName == className && x.MethodName == methodName));
            foundMetrics.Add(await db.TaskMetrics.SingleOrDefaultAsync(x => x.ClassName == className && x.MethodName == methodName));
            foundMetrics.Add(await db.ExceptionMetrics.SingleOrDefaultAsync(x => x.ClassName == className && x.MethodName == methodName));

            foundMetrics.ForEach(x=>x.sta)
        }

        private GetMetricTable()
    }
}
