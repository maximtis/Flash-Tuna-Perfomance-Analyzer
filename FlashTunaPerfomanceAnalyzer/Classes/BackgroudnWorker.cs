using FlashTuna.Core.Modules.Runtime;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlashTunaPerfomanceAnalyzer.Classes
{
    internal class NotificationServie : IHostedService, IDisposable
    {
        private Timer _timer;

        public NotificationServie(IHubContext<NotifyHub, RuntimeMetricsNotification> hubContext)
        {
            _hubContext = hubContext;
        }
        private IHubContext<NotifyHub, RuntimeMetricsNotification> _hubContext;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _hubContext?.Clients.All.MetricsUpdatedBroadcast().Wait();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
