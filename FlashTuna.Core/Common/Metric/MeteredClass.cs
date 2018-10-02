using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric.Interfaces;
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
        private string _derivedClassName;

        public MeteredClass([CallerMemberName] string className = null)
        {
            _derivedClassName = className;
        }

        public void StartRecording([CallerMemberName] string methodName = null)
        {
            _timeLine.StartMetric(_derivedClassName, methodName);
        }
        public void StopRecording([CallerMemberName] string methodName = null)
        {
            _timeLine.StopMetric(_derivedClassName, methodName);
        }
    }
}
