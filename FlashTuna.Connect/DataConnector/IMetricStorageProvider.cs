using FlashTuna.Core.Common.TimeLine;
using FlashTuna.Data.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Connect.DataProvider
{
    public interface IMetricStorageTimeLineConnector
    {
        ITimeLine TimeLine { get; }
        IMetricStorage MetricStorage { get; }
    }
}
