using FlashTuna.Core.Common.Metric.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlashTuna.Core.Common.Metric
{
    public abstract class BaseErrorResult : IErrorResult
    {
        public BaseErrorResult()
        {

        }
        public BaseErrorResult(string exceptionMessage,
                               string exceptionType,
                               string moduleName,
                               string className,
                               string methodName,
                               string tag)
        {
            TimePoint = DateTime.Now;
            ModuleName = moduleName;
            ClassName = className;
            MethodName = methodName;
            Tag = tag;
            ExceptionMessae = exceptionMessage;
            ExceptionType = exceptionType;
            //Collect Start Data
        }

        public string Tag { get; set; }
        public string ModuleName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionMessae { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime TimePoint { get; set; }
    }
}
