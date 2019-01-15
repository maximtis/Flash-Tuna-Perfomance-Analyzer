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
    public class ServerWithErrors:MeteredClass,IServer
    {
        public ServerWithErrors():base(typeof(ServerWithErrors)) {
        }

        [OperationMetric(nameof(ServerWithErrors),"WithErrors")]
        public async Task DetectUserAccessAsync(User user)
        {
            UsersRequests.Add(user);
            using (await StartRecording())
            {
                try
                {
                    UsersRequests.Add(user);
                    await InvalidateUserData();
                }
                catch(Exception ex)
                {
                    await RecordExeption(ex);
                }
                
            }
        }

        private async Task InvalidateUserData()
        {
            int errorStart = 7;
            if (UsersRequests.Count > errorStart)
            {
                throw new InvalidOperationException("ErrorInvalidateUser");
            }
            foreach(var user in UsersRequests)
            {
                user.Data = new Random().Next(1, 999999);
                Thread.Sleep(100);
            }
            await Task.CompletedTask;
        }

        public async Task SomeAction(User user)
        {
            await DetectUserAccessAsync(user);
        }

        public List<User> UsersRequests { get; set; } = new List<User>();
    }
}
