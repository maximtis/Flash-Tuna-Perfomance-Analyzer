﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashTuna.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationMetricResults",
                columns: table => new
                {
                    Tag = table.Column<string>(nullable: true),
                    ModuleName = table.Column<string>(nullable: true),
                    ClassName = table.Column<string>(nullable: true),
                    MethodName = table.Column<string>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimePoint = table.Column<DateTime>(nullable: false),
                    Milliseconds = table.Column<double>(nullable: true),
                    MetricResultStatus = table.Column<int>(nullable: false),
                    StartTimePoint = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationMetricResults", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationMetricResults");
        }
    }
}
