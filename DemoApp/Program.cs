using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.Metric.MetricsFactory;
using System;
using System.Threading;

namespace DemoApp
{
    class Program
    {
        private static void LongOperation()
        {
            int i = 10;
            while (i > 0)
            {
                Console.WriteLine("Do Iteration!");
                Thread.Sleep(1000);
                i--;
            }
        }
        static void Main(string[] args)
        {
            IMetric mainTest = MetricsFactory.CreateMetric("Main", "test", "Demo Project");
            
            Console.WriteLine("Demo App started...");

            mainTest.Start();
            LongOperation();
            mainTest.Stop();

            Console.WriteLine("Demo App finished...");
            Console.ReadKey();

        }
    }
}
