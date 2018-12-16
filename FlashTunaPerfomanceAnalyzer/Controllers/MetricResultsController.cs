using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.Configuration;
using FlashTunaPerfomanceAnalyzer.Classes.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlashTunaPerfomanceAnalyzer.Controllers
{
    [Route("api/[controller]")]
    public class MetricResultsController : Controller
    {
        [HttpGet("[action]")]
        public async Task<List<MetricResultViewModel>> GetMetricsResults(MetricsResultsRequest model)
        {
            var results = await FlashTunaAnalyzer.Results
                                                 .GetResultsByPeriod(model.PeriodFrom, model.PeriodTo, model.MethodName);
            return results;
        }

        [HttpGet("[action]")]
        public async Task<List<string>> GetMetrics(MetricsResultsRequest model)
        {
            var results = await FlashTunaAnalyzer.Results
                                                 .GetMetricsByPeriod(model.PeriodFrom, model.PeriodTo);
            return results;
        }

    }
}
