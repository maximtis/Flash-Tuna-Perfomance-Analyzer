using FlashTuna.Attributes;
using System;
using System.Threading;
using System.Linq;

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
            var _perfomanceMethods = this.GetType().GetMethods().Where(x => x.GetCustomAttributes(typeof(PerfomanceMetricAttribute), true).Any());
            _perfomanceMethods.First().Name
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
