using DemoApp.ScenarioInvoker;
using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Common.Metric;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.ServerWithLoad
{
    public class ServerChaoticTime : MeteredClass, IServer
    {
        public ServerChaoticTime():base(typeof(ServerChaoticTime)) { }

        [OperationMetric(nameof(ServerChaoticTime),"WithLoad")]
        public async Task UserDataProcAsync(User user)
        {
            using (await StartRecording())
            {
                UsersRequests.Add(user);
                await InvalidateUserData();
            }
        }

        private async Task InvalidateUserData()
        {
            var time = new Random(Environment.TickCount).Next(1, 300);
            foreach (var user in UsersRequests)
            {
                user.Data = new Random().Next(1, 999999);
                Thread.Sleep(time);
            }
            await Task.CompletedTask;
        }

        public async Task SomeAction(User user)
        {
            await UserDataProcAsync(user);
        }

        public List<User> UsersRequests { get; set; } = new List<User>();
    }
}
