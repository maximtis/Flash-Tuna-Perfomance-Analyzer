using FlashTuna.Core.Common.PerfomanceMetrics.ExceptionMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.TaskMetric;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Storage.DataBase
{
    public class FlashTunaDbContext : DbContext, IFlashTunaDbContext
    {
        FlashTunaDbContext() : base(){}

        public DbSet<OperationMetric> OperationMetrics { get; set; }
        public DbSet<OperationMetricResult> OperationMetricResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Metrics.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
