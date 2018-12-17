using System;

namespace FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric
{
    public class MetricResultViewModel
    {
        public string Tag { get; set; }
        public string ModuleName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public long Milliseconds { get; set; }
        public DateTime StartPoint { get; set; }
        public DateTime EndPoint { get; set; }
        public Guid Id { get; set; }
    } 
}
