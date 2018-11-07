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

namespace DemoApp
{

    public class DataBaseRepository : MeteredClass
    {
        public DBContext db;

        public DataBaseRepository() : base(typeof(DataBaseRepository))
        {
            db = new DBContext();
        }

        [TaskMetric(nameof(DataBaseRepository))]
        [OperationMetric(nameof(DataBaseRepository))]
        public List<User> GetUsersAgeGreterThan(int targetAge)
        {
            using (StartRecording())
            {
                var users = db.Users.Where(user => user.Age > targetAge).ToList();
                return users;
            }
        }

        [TaskMetric(nameof(DataBaseRepository))]
        [OperationMetric(nameof(DataBaseRepository))]
        [ExceptionsMetric(nameof(DataBaseRepository))]
        public User GetUserByName(string name)
        {
            using (StartRecording())
            {
                var targetUser = db.Users.Single(user => user.Name == name);
                return targetUser;
            }
        }
    }




    class ProductionClassB : MeteredClass
    {
        public ProductionClassB() : base(typeof(ProductionClassB))
        {

        }

        [OperationMetric(nameof(ProductionClassB))]
        public void LongOperation()
        {
            StartRecording();
            int i = 10;
            while (i > 0)
            {
                Console.WriteLine("Do Iteration!");
                Thread.Sleep(1000);
                i--;
            }
            StopRecording();
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

            StopRecording();
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            FlashTuna.Core.Configuration.FlashTuna.Initialize(
                            FlashTuna.Core.Configuration.FlashTuna.CreateBuilder()
                                                .SetStorage(null)
                                                .SetModuleName("Test Module")
                                                .SetTargetAssembly(typeof(Program).Assembly)
                                                .Build());

            ProductionClassA classA = new ProductionClassA();
            //classA.LongOperation();

            ProductionClassB classB = new ProductionClassB();
            List<Task> tasks = new List<Task>();
            for (int i = 0;i < 10; i++)
            {
                tasks.Add(Task.Run(() => classB.ShortOperation()));
            }
            Task.WaitAll(tasks.ToArray());
            //classB.LongOperation();

            print();

        }

        static void print()
        {
            var data = FlashTuna.Core.Configuration.FlashTuna.PrintMetricsResult().Result;
            Console.WriteLine(data);
            Console.ReadKey();
        }
    }
}
