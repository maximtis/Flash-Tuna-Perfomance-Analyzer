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

    internal sealed class TimeLine : ITimeLine
    {
        private IFlashTunaDbContext db = null;
        private HashSet<IMetric> _metrics = new HashSet<IMetric>();
        private List<IMetricResult> _metricsResults = new List<IMetricResult>();

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
            targetMetric?.Start();
        }

        public async Task StopMetric(string className, string methodName)
        {
            var targetMetric = _metrics.SingleOrDefault(x => x.ClassName == className && x.MethodName == methodName);
            targetMetric?.Stop();
        }

        public async Task CollectMetricResult(IMetricResult metricResult)
        {
            _metricsResults.Add(metricResult);
        }

        public async Task<IEnumerable<IMetricResult>> ExtractMetricResult()
        {
            return _metricsResults.OrderBy(x => x.StartTime); 
        }
    }
}
