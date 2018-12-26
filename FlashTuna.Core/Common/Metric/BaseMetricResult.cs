using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlashTuna.Core.Common.Metric
{
    public abstract class BaseMetricResult: IMetricResult
    {
        public BaseMetricResult()
        {

        }
        public BaseMetricResult(int status,
                                string moduleName,
                                string className,
                                string methodName,
                                string tag,
                                Guid callId)
        {
            TimePoint = DateTime.Now;
            MetricResultStatus = status;
            ModuleName = moduleName;
            ClassName = className;
            MethodName = methodName;
            Tag = tag;
            CallId = callId;
            //Collect Start Data
        }
        public string Tag { get; set; }
        public string ModuleName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public Guid CallId { get; set; }
        public DateTime TimePoint { get; set; }
        public double? Milliseconds { get; set; }
        public int MetricResultStatus { get; set; }

        public virtual string ToMetricString()
        {
            return $"Status:{MetricResultStatus.ToString()} - {ClassName}:{MethodName} \n {TimePoint.ToShortTimeString()} ({Milliseconds} ms - {(Milliseconds.HasValue? (Milliseconds / 1000):0)} s)";
        }
    }
}