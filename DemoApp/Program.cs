using System;
using System.Threading;
using System.Linq;
using FlashTuna.Core.Common.Metric.Interfaces;
using FlashTuna.Core.Common.Metric.MetricsFactory;
using FlashTuna.Core.TimeLine;
using FlashTuna.Core.Common.Metric;
using FlashTuna.Core.Attributes;

namespace DemoApp
{

    class ProductionClassA : MeteredClass
    {
        [OperationMetric(nameof(ProductionClassA))]
        void LongOperation()
        {
            int i = 10;
            while (i > 0)
            {
                Console.WriteLine("Do Iteration!");
                Thread.Sleep(1000);
                i--;
            }
        }

    }


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
            
            Console.WriteLine("Demo App started...");
            LongOperation();
            Console.WriteLine("Demo App finished...");
            Console.ReadKey();

        }
    }
}
