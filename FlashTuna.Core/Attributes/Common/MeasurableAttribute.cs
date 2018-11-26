using FlashTuna.Core.Common.Metric.MetricsFactory;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FlashTuna.Core.Attributes.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MeasurableAttribute : Attribute
    {
        public MeasurableAttribute(string className, [CallerMemberName] string methodName = null, string tag = null) : base()
        {
            MetricsFactory.CreateMetric(className, methodName, tag);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoMeasurableAttribute : Attribute
    {
        public NoMeasurableAttribute(string className, [CallerMemberName] string methodName = null, string tag = null) : base()
        {
            // Ignored
        }
    }
}
