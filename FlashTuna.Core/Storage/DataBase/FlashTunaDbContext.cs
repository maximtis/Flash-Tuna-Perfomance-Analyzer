﻿using FlashTuna.Core.Common.PerfomanceMetrics.OperationMetric;
using FlashTuna.Core.Common.PerfomanceMetrics.OperitionMetric;
using FlashTuna.Core.Modules.Errors;
using FlashTuna.Core.Modules.Usage;
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
        public DbSet<TrackedMethod> TrackedMethods { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ErrorResult> ErrorResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Metrics;Trusted_Connection=True;");
            //optionsBuilder.UseSqlite("Data Source=Metrics.db");
            //DbContextOptionsBuilder builder = new DbContextOptionsBuilder().UseSqlite( )
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>()
                .HasMany(x => x.TrackedMethods);
        }
    }

}