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

        static MeteredClass()
        {
            var ass = Assembly.GetExecutingAssembly();//Use .GetCallingAssembly() if this method is in a library, or even both
               var types = ass.GetTypes();
            var methods = types.SelectMany(t => t.GetMethods());
                   var hasAttr = methods.Where(m => m.GetCustomAttributes(typeof(OperationMetricAttribute), true).Length > 0)
                      .ToList();
        }
        public MeteredClass(Type derivedClass)
        {
            _timeLine = FlashTuna.Core.Configuration.FlashTuna.CurrentTimeLine;
            _derivedClassName = derivedClass;
            Assembly clientAssembly = derivedClass.Assembly;

            var ass = clientAssembly; //Assembly.GetExecutingAssembly();//Use .GetCallingAssembly() if this method is in a library, or even both
            var types = ass.GetTypes();
            var methods = types.SelectMany(t => t.GetMethods());
            var hasAttr = methods.Where(m => m.GetCustomAttributes(typeof(OperationMetricAttribute), true).Length > 0)
               .ToList();
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
