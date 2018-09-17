using System;

namespace FlashTuna.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PerfomanceMetricAttribute : Attribute
    {
        public PerfomanceMetricAttribute()
        {
        }
    }
}
