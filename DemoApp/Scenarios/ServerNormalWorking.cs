using DemoApp.ScenarioInvoker;
using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.ServerWithLoad
{
    public class ServerNormalWorking:MeteredClass, IServer
    {
        public ServerNormalWorking():base(typeof(ServerNormalWorking)) { }

        [OperationMetric(nameof(ServerNormalWorking), "Normal")]
        public async Task UpdateUserDataAsync(User user)
        {
            using (await StartRecording())
            {
                UsersRequests.Add(user);
                await InvalidateUserData();
            }
        }

        private async Task InvalidateUserData()
        {
            var time = new Random().Next(1, 50);

            var user = UsersRequests.Last();
                user.Data = time;
            Thread.Sleep(time);
            await Task.CompletedTask;
        }

        public async Task SomeAction(User user)
        {
            await UpdateUserDataAsync(user);
        }

        public List<User> UsersRequests { get; set; } = new List<User>();
    }
}
