using FlashTuna.Core.TimeLine;
using FlashTuna.Data.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.DataConnector
{
    public interface IMetricStorageTimeLineConnector
    {
        void ConnectTimeLine(ITimeLine timeLine);
        void ConnectMetricStorage(ITimeLine timeLine);

        ITimeLine TimeLine { get; }
        IMetricStorage MetricStorage { get; }
    }
}
