using System;
using System.Collections.Generic;
using System.Text;
using FlashTuna.Data.Storage;

namespace FlashTuna.Connect.DataProvider
{
    public class MetricStorageProvider : IMetricStorageProvider
    {
        IMetricStorage _metricsStorage;
        public MetricStorageProvider(IMetricStorage metricsStorage)
        {
            _metricsStorage = metricsStorage;
        }
        public IMetricStorage MetricStorage { get => _metricsStorage; }
    }
}
