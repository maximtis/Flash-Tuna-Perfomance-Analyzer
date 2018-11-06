using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.Metric.Interfaces
{
    public interface IMetricCall:IDisposable
    {
        void Stop();
    }
}
