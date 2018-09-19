using System;
using System.Collections.Generic;
using System.Text;
using FlashTuna.Core.Common.TimeLine;
using FlashTuna.Data.Storage;

namespace FlashTuna.Connect.DataProvider
{
    public class MetricStorageTimeLineConnector : IMetricStorageTimeLineConnector
    {
        IMetricStorage _metricsStorage;
        public MetricStorageProvider(IMetricStorage metricsStorage)
        {
            _metricsStorage = metricsStorage;
        }
        public IMetricStorage MetricStorage { get => _metricsStorage; }

        public ITimeLine TimeLine => throw new NotImplementedException();
    }
}
