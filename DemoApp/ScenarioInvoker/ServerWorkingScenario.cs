using DemoApp.ScenarioInvoker;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.ServerWithLoad
{
    public class ServerWorkingScenario
    {
        public void InitializeAndRunScenarios()
        {
            List<Task> tasks = new List<Task>();
            List<IServer> servers = new List<IServer>();
            IServer normalServer = new ServerNormalWorking();
            IServer loadServer = new ServerWithLoad();
            IServer chaoticServer = new ServerChaoticTime();
            IServer errorsServer = new ServerWithErrors();
            servers.Add(normalServer);
            servers.Add(loadServer);
            servers.Add(chaoticServer);
            servers.Add(errorsServer);

            foreach(var server in servers)
            {
                tasks.Add(
                    Task.Run(async () =>
                    await RunScenario(server)
                    ));
            }
            Task.WaitAll(tasks.ToArray());

        }
        private async Task RunScenario(IServer server)
        {
            int requestCount = 20;
            while(--requestCount > 1)
            {
                await server.SomeAction(new User());
            }
        }
    }
}
