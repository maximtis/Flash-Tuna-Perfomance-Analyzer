using System;
using System.Collections.Generic;
using System.Text;
using FlashTuna.Core.DataConnector;
using FlashTuna.Core.TimeLine;
using FlashTuna.Data.Storage;

namespace FlashTuna.Connect.DataProvider
{
    public class MetricStorageTimeLineConnector : IMetricStorageTimeLineConnector
    {
        public MetricStorageTimeLineConnector(IMetricStorage metricsStorage)
        {
            MetricStorage = metricsStorage;
        }
        public IMetricStorage MetricStorage { get; }

        public ITimeLine TimeLine => throw new NotImplementedException();

        public void ConnectMetricStorage(ITimeLine timeLine)
        {
            throw new NotImplementedException();
        }

        public void ConnectTimeLine(ITimeLine timeLine)
        {
            throw new NotImplementedException();
        }
    }
}
