using FlashTuna.Attributes;
using System;
using System.Threading;
using System.Linq;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.Metric.MetricsFactory;
using FlashTuna.Core.TimeLine;

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
        public Program()
        {
            var _perfomanceMethods = this.GetType().GetMethods().Where(x => x.GetCustomAttributes(typeof(TaskMetricAttribute), true).Any());
        }
        static void Main(string[] args)
        {
            ITimeLine tl = new TimeLine(null);
            IMetric mainTest = MetricsFactory.CreateMetric(tl,"Main", "test", "Demo Project");
            
            Console.WriteLine("Demo App started...");

            mainTest.Start();
            LongOperation();
            mainTest.Stop();

            Console.WriteLine("Demo App finished...");
            Console.ReadKey();

        }
    }
}
