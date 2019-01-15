using DemoApp.ServerWithLoad;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.ScenarioInvoker
{
    public interface IServer
    {
        Task SomeAction(User user);
    }
}
