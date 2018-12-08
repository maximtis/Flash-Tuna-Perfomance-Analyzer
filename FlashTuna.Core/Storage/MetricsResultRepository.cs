using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.Storage.DataBase;
using FlashTuna.Core.TimeLine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.Storage
{
    public class MetricsResultRepository
    {
        public async Task<IEnumerable<IMetricResult>> GetResultsByPeriod(DateTime from, DateTime? to = null)
        {
            using (FlashTunaDbContext db = new FlashTunaDbContext())
            {
                to = to ?? DateTime.Now;
                return await db.OperationMetricResults
                               .Where(o => o.TimePoint >= from && o.TimePoint <= to)
                               .OrderBy(o => o.TimePoint)
                               .ToListAsync();
            }
        }
    }
}
