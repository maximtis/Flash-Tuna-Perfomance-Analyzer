using System;
using System.Threading;
using System.Linq;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.Metric.MetricsFactory;
using FlashTuna.Core.TimeLine;
using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Attributes;
using System.Threading.Tasks;
using System.Collections.Generic;
using FlashTuna.Core.Storage.DataBase;
using Microsoft.AspNetCore.SignalR;
using DemoApp.ServerWithLoad;

namespace DemoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FlashTuna.Core.Configuration.FlashTunaAnalyzer.Initialize(
                            FlashTuna.Core.Configuration.FlashTunaAnalyzer.CreateBuilder()
                                                .SetStorage()
                                                .SetModuleName("Test Module")
                                                .SetTargetAssembly(typeof(Program).Assembly)
                                                .Build());
            ServerWorkingScenario serverWorkingScenario = new ServerWorkingScenario();
            serverWorkingScenario.InitializeAndRunScenarios();
            //ProductionClassA classA = new ProductionClassA();
            //classA.LongOperation().Wait();

            //ProductionClassB classB = new ProductionClassB();
            //List<Task> tasks = new List<Task>();
            //for (int i = 0;i < 10; i++)
            //{
            //    tasks.Add(Task.Run(async () => await classB.ShortOperation()));
            //    tasks.Add(Task.Run(async () => await classB.LoginOperation()));
            //    tasks.Add(Task.Run(async () => await classB.PingTestMethod()));
            //    tasks.Add(Task.Run(async () => await classB.RemoveUser()));
            //    tasks.Add(Task.Run(async () => await classB.TestOperation()));
            //}
            //Task.WaitAll(tasks.ToArray());
            //classB.ShortOperation().Wait();

            //print();

        }

        static void print()
        {
            var data = FlashTuna.Core.Configuration.FlashTunaAnalyzer.PrintMetricsResult().Result;
            Console.WriteLine(data);
            Console.ReadKey();
        }
    }
}
