using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using FlashTuna.Core.DataConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.TimeLine
{ 

    public sealed class TimeLine : ITimeLine
    {

        public TimeLine(IMetricStorageTimeLineConnector storageProvider){

        }

        public Task BoundMetric(IMetric metric)
        {
            throw new NotImplementedException();
        }

        public Task CollectMetricResult(IMetricResult metric)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMetric>> GetBoundMetrics()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMetricResult>> GetMetricResult(string tag)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMetricResult>> GetMetricResult(MetricTypes metricsType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMetricResult>> GetMetricResult(MetricTypes metricsType, string methodName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMetricResult>> GetMetricResult(MetricTypes metricsType, string methodName, string moduleName)
        {
            throw new NotImplementedException();
        }
    }
}
