using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.Common.Metric.Interfaces
{
    public interface IMetric
    {
        void Start();
        void Stop();
        bool isRunning { get; set; }
        bool isUsed { get; set; }
    }
}
