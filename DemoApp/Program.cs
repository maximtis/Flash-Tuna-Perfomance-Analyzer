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
        public ProductionClassA() : base(typeof(ProductionClassA))
        {

        }
        [OperationMetric(nameof(ProductionClassA))]
        public void LongOperation()
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

    class ProductionClassB : MeteredClass
    {
        public ProductionClassB() : base(typeof(ProductionClassA))
        {

        }

        [OperationMetric(nameof(ProductionClassB))]
        public void LongOperation()
        {
            int i = 10;
            while (i > 0)
            {
                Console.WriteLine("Do Iteration!");
                Thread.Sleep(1000);
                i--;
            }
        }

        [OperationMetric(nameof(ProductionClassB))]
        public void ShortOperation()
        {
            int i = 10;
            StartRecording();
            while (i > 0)
            {
                Console.WriteLine("Do Iteration!");
                Thread.Sleep(100);
                i--;
            }
        }

    }


    class Program
    {
        
        public Program()
        {
            var _perfomanceMethods = this.GetType().GetMethods().Where(x => x.GetCustomAttributes(typeof(TaskMetricAttribute), true).Any());
        }
        static void Main(string[] args)
        {

            FlashTuna.Core.Configuration.FlashTuna.Initialize(
                            new FlashTuna.Core.Configuration.FlashTuna.FlashTunaBuilder()
                                                            .SetTimeLine(new TimeLine(null))
                                                            .SetModuleName("Test Module")
                                                            .Build());

            ProductionClassA classA = new ProductionClassA();
            classA.LongOperation();

            ProductionClassB classB = new ProductionClassB();
            classB.ShortOperation();
            classB.LongOperation();
        }
    }
}
