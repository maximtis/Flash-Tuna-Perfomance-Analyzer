using FlashTuna.Core.Attributes;
using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Modules.Errors;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.Common.Metric
{
    public abstract class MeteredClass
    {
        private ITimeLine _timeLine;
        private Type _derivedClassName;

        public MeteredClass(Type derivedClass)
        {
            _timeLine = FlashTuna.Core.Configuration.FlashTunaAnalyzer.CurrentTimeLine;
            _derivedClassName = derivedClass;
        }

        protected async Task<IMetricCall> StartRecording([CallerMemberName] string methodName = null)
        {
            return await _timeLine.StartMetricAsync(_derivedClassName.Name, methodName);
        }
        protected async Task RecordExeption(Exception ex,[CallerMemberName] string methodName = null,bool rethrow = false)
        {
            IErrorResult result = new ErrorResult(ex.Message,
                                                  ex.GetType().Name,
                                                  FlashTuna.Core.Configuration.FlashTunaAnalyzer.ModuleName,
                                                  _derivedClassName.Name, 
                                                  methodName, 
                                                  null);
            await _timeLine.CollectException(result);
            if (rethrow)
            {
                throw new Exception("Exception hanled by FlashTuna.", ex);
            }
        }

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
        // ~MeteredClass() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
