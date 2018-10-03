using FlashTuna.Core.Attributes;
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
        private Type _derivedClassName;
        //private List<MethodInfo> _meteredMethods;

        public MeteredClass(Type derivedClass)
        {
            _timeLine = FlashTuna.Core.Configuration.FlashTuna.CurrentTimeLine;
            _derivedClassName = derivedClass;
            //_meteredMethods = FlashTuna.Core.Configuration.FlashTuna.MeteredMethods.Where(x=>derivedClass.Name == x.DeclaringType.Name).ToList();
        }

        protected void StartRecording([CallerMemberName] string methodName = null)
        {
            _timeLine.StartMetric(_derivedClassName.Name, methodName);
        }

        protected void StopRecording([CallerMemberName] string methodName = null)
        {
            _timeLine.StopMetric(_derivedClassName.Name, methodName);
        }
    }
}
