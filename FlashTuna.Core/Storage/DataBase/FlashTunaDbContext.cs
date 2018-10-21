using FlashTuna.Core.Common.PerfomanceMetrics.ExceptionMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.TaskMetric;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Storage.DataBase
{
    public class FlashTunaDbContext : DbContext
    {
        FlashTunaDbContext() : base(){}

        public DbSet<ExceptionMetric> ExceptionMetrics { get; set; }
        public DbSet<ExceptionMetric> OperationMetrics { get; set; }
        public DbSet<ExceptionMetric> TaskMetrics { get; set; }
        public DbSet<ExceptionMetricResult> ExceptionMetricResults { get; set; }
        public DbSet<OperationMetricResult> OperationMetricResults { get; set; }
        public DbSet<TaskMetricResult> TaskMetricResults { get; set; }
    }
}
