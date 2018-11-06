using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FlashTuna.Core.Common.Metric
{
    public abstract class BaseMetricCall : IMetricCall, IDisposable
    {
        public BaseMetricCall(string className,
                              string methodName,
                              MetricTypes metricType,
                              ITimeLine timeLine)
        {
            _boundedTimeLine = timeLine;
            _metricType = metricType;
            _className = className;
            _methodName = methodName;
            _timePoint = DateTime.Now;
            _metricResultStatus = MetricResultStatus.Started;
            //Collect Start Data
            _boundedTimeLine.CollectMetricResult(GetResult());
        }

        protected MetricResultStatus _metricResultStatus;
        protected string _className;
        protected string _methodName;
        protected DateTime _timePoint;
        protected ITimeLine _boundedTimeLine;
        protected MetricTypes _metricType;

        public virtual void Stop()
        {
            _boundedTimeLine.CollectMetricResult(GetResult());
        }
        protected abstract IMetricResult GetResult();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MetricCallBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

