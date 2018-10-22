using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Attributes.Common
{
    public class MetricKey
    {
        public long MetricCallId { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
    }
}
