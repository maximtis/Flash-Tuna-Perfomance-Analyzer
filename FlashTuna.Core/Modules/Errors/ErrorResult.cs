using FlashTuna.Core.Common.Metric;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Modules.Errors
{
    public class ErrorResult : BaseErrorResult
    {
        public ErrorResult()
        {

        }
        public ErrorResult(string exceptionType,
                            string exceptionMessage,
                               string moduleName,
                               string className,
                               string methodName,
                               string tag) : base(exceptionMessage, exceptionType, moduleName, className, methodName, tag)
        {

        }
    }
}
