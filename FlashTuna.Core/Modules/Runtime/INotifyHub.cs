using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.Modules.Runtime
{
    public interface RuntimeMetricsNotification
    {
        Task MetricsUpdatetdBroadcast(string methodName);
    }
    public class NotifyHub:Hub<RuntimeMetricsNotification>
    {
    }
}
