using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using FlashTuna.Core.Storage.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.TimeLine
{ 

    public sealed class TimeLine : ITimeLine
    {
        IFlashTunaDbContext db = null;
        HashSet<IMetric> _metrics = new HashSet<IMetric>();
        public TimeLine(IFlashTunaDbContext storageProvider){
            db = storageProvider;
        }

        public async Task BoundMetric(IMetric metric)
        {
            _metrics.Add(metric);
        }

        public async Task StartMetric(string className, string methodName)
        {
            var targetMetric = _metrics.SingleOrDefault(x => x.ClassName == className && x.MethodName == methodName);
            targetMetric.Start();
        }

        public async Task StopMetric(string className, string methodName)
        {
            var targetMetric = _metrics.SingleOrDefault(x => x.ClassName == className && x.MethodName == methodName);
            targetMetric.Stop();
        }

        public async Task<IEnumerable<IMetricResult>> CollectMetricResult(IMetricResult metric)
        {
            var metricsResult = _metrics.Select(x => x.GetResult());
            return metricsResult;
        }
    }
}
