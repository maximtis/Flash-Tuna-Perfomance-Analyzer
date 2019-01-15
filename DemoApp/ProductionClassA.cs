using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp
{
    internal class ProductionClassA:MeteredClass
    {
        public ProductionClassA() : base(typeof(ProductionClassA))
        {

        }

        [OperationMetric(nameof(ProductionClassA))]
        public async Task LongOperation()
        {
            using (await StartRecording())
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("LongOperation");
                }
            }
            await Task.FromResult(0);
        }
    }
}