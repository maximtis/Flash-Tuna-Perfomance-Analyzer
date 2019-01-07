using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp
{
    internal class ProductionClassB:MeteredClass
    {
        bool throwEx = true;
        public ProductionClassB() : base(typeof(ProductionClassB))
        {

        }
        [OperationMetric(nameof(ProductionClassB))]
        public async Task ShortOperation()
        {
            using (await StartRecording())
            {
                try
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Thread.Sleep(100);
                        Console.WriteLine("ShortOperation");
                        if (throwEx)
                        {
                            throwEx = !throwEx;
                            throw new ArgumentException("TestArgumentEx");
                        }
                    }
                }
                catch(Exception ex)
                {
                    await RecordExeption(ex);
                }
                
            }
            await Task.FromResult(0);
        }

        [OperationMetric(nameof(ProductionClassB))]
        public async Task LoginOperation()
        {
            using (await StartRecording())
            {
                try
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var sleepTime = new Random(Environment.TickCount).Next(0, 500);
                        Thread.Sleep(sleepTime);
                        Console.WriteLine("LoginOperation");
                        if (throwEx)
                        {
                            throwEx = !throwEx;
                            throw new InvalidCastException("TestInvalidCastException");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await RecordExeption(ex);
                }
            }
            await Task.FromResult(0);
        }

        [OperationMetric(nameof(ProductionClassB))]
        public async Task TestOperation()
        {
            using (await StartRecording())
            {
                try { 
                    for (int i = 0; i < 5; i++)
                    {
                        var sleepTime = new Random(Environment.TickCount).Next(0, 200);
                        Thread.Sleep(sleepTime);
                        Console.WriteLine("TestOperation");
                        if (throwEx)
                        {
                            throwEx = !throwEx;
                            throw new OperationCanceledException("TestOperationCanceledException");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await RecordExeption(ex);
                }
            }
            await Task.FromResult(0);
        }

        [OperationMetric(nameof(ProductionClassB))]
        public async Task RemoveUser()
        {
            using (await StartRecording())
            {
                try { 
                    for (int i = 0; i < 5; i++)
                    {
                        var sleepTime = new Random(Environment.TickCount).Next(0, 300);
                        Thread.Sleep(sleepTime);
                        Console.WriteLine("RemoveUser");
                        if (throwEx)
                        {
                            throwEx = !throwEx;
                            throw new TimeoutException("TestTimeoutException");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await RecordExeption(ex);
                }
            }
            await Task.FromResult(0);
        }

        [OperationMetric(nameof(ProductionClassB))]
        public async Task PingTestMethod()
        {
            using (await StartRecording())
            {
                try
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var sleepTime = new Random(Environment.TickCount).Next(0, 700);
                        Thread.Sleep(sleepTime);
                        Console.WriteLine("PingTestMethod");
                        if (throwEx)
                        {
                            throwEx = !throwEx;
                            throw new NullReferenceException("TestNullReferenceException");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await RecordExeption(ex);
                }
            }
            await Task.FromResult(0);
        }
    }
}