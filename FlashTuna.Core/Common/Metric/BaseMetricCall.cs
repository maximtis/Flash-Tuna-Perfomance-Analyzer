using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FlashTuna.Core.Common.Metric
{
    public abstract class BaseMetricCall : IMetricCall, IDisposable
    {
        public BaseMetricCall(string moduleName,
                              string className,
                              string methodName,
                              string tag,
                              ITimeLine timeLine)
        {
            _boundedTimeLine = timeLine;
            _className = className;
            _methodName = methodName;
            _timePoint = DateTime.Now;
            _metricResultStatus = (int)MetricResultStatus.Started;
            _callId = Guid.NewGuid();
            //Collect Start Data
            _boundedTimeLine.CollectMetricResult(GetResult((int)MetricResultStatus.Started,_callId));
        }

        protected int _metricResultStatus;
        protected ITimeLine _boundedTimeLine;
        protected DateTime _timePoint;
        protected string _moduleName;
        protected string _className;
        protected string _methodName;
        protected string _tag;
        protected Guid _callId;

        public virtual void Stop()
        {
            _boundedTimeLine.CollectMetricResult(GetResult((int)MetricResultStatus.Stopped, _callId));
        }
        protected abstract IMetricResult GetResult(int status,Guid callId);

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
                Stop();
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

