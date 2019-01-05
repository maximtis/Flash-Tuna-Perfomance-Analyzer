using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Common.Metric.Interfaces
{
    public interface IErrorResult
    {
        DateTime TimePoint { get; set; }
        string ClassName { get; set; }
        string MethodName { get; set; }
        string Tag { get; set; }
        string ModuleName { get; set; }
    }
}
