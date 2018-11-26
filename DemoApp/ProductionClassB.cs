using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using System.Threading;

namespace DemoApp
{
    internal class ProductionClassB:MeteredClass
    {
        public ProductionClassB() : base(typeof(ProductionClassB))
        {

        }
        [OperationMetric(nameof(ProductionClassB))]
        public void ShortOperation()
        {
            for(int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
            }
        }
    }
}