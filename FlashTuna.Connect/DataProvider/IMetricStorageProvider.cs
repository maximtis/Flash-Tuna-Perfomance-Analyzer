using FlashTuna.Data.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Connect.DataProvider
{
    public interface IMetricStorageProvider
    {
        IMetricStorage MetricStorage { get; }
    }
}
