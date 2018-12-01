using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Storage.DataBase
{
    public class FlashTunaDbContext : DbContext, IFlashTunaDbContext
    {
        public FlashTunaDbContext() : base(){}
        public DbSet<OperationMetricResult> OperationMetricResults { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Metrics.db");
        }
    }
}
