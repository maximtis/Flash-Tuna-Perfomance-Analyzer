using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.Configuration;
using FlashTuna.Core.Modules.Runtime;
using FlashTunaPerfomanceAnalyzer.Classes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FlashTunaPerfomanceAnalyzer.Controllers
{
    [Route("api/[controller]")]
    public class MetricResultsController : Controller
    {
        public MetricResultsController(IHubContext<NotifyHub, RuntimeMetricsNotification> hubContext)
        {   
            _hubContext = hubContext;
        }
        private IHubContext<NotifyHub, RuntimeMetricsNotification> _hubContext;

        [HttpPost("[action]")]
        public async Task<List<MetricResultViewModel>> GetMetricsResults([FromBody] MetricsResultsRequest model)
        {
            var results = await FlashTunaAnalyzer.Results
                                                 .GetResultsByPeriod(model.PeriodFrom, model.PeriodTo, model.MethodName);
            return results;
        }

        [HttpPost("[action]")]
        public async Task<List<string>> GetMetrics([FromBody] MetricsResultsRequest model)
        {
            var results = await FlashTunaAnalyzer.Results
                                                 .GetMetricsByPeriod(model.PeriodFrom, model.PeriodTo);
            return results;
        }
    }
}
