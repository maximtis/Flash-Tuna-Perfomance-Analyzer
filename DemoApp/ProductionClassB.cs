using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp
{
    internal class ProductionClassB:MeteredClass
    {
        public ProductionClassB() : base(typeof(ProductionClassB))
        {

        }
        [OperationMetric(nameof(ProductionClassB))]
        public async Task ShortOperation()
        {
            using (await StartRecording())
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Console.WriteLine("ShortOperation");
                }
            }
            await Task.FromResult(0);
        }
    }
}