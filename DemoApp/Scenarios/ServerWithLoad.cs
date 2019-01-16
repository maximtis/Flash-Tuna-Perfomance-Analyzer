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
    public class ServerWithLoad:MeteredClass, IServer
    {
        public ServerWithLoad():base(typeof(ServerWithLoad)) { }

        [OperationMetric(nameof(ServerWithLoad),"WithLoad")]
        public async Task ProcessUserReqAsync(User user)
        {
            using (await StartRecording())
            {
                UsersRequests.Add(user);
                InvalidateUserData().Wait();
            }
        }
        #region helpers
        private async Task InvalidateUserData()
        {
            var time = 100;
            foreach(var user in UsersRequests)
            {
                time += 100;
            }
            Thread.Sleep(time);
            await Task.CompletedTask;
        }

        public async Task SomeAction(User user)
        {
            ProcessUserReqAsync(user).Wait();
        }

        public List<User> UsersRequests { get; set; } = new List<User>();
    }

    public class User
    {
        public int Data { get; set; }
    }
    #endregion;
}
