using System;
using System.Threading;
using System.Threading.Tasks;
using DeviceStateTestTask.WebService.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace DeviceStateTestTask.WebService.Services
{
    public class TrackHostedService: BackgroundService
    {
        private readonly IHubContext<TrackHub> _hubContext;

        public TrackHostedService(IHubContext<TrackHub> hubContext)
        {
            this._hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {        
            await this.SendMessages(stoppingToken);
        }

        private async Task SendMessages(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {                
                await this._hubContext.Clients.All.SendAsync(TrackHub.SEND_MESSAGE_TYPE); 
                await Task.Delay(1000 * 5, stoppingToken);
            }
        }
    }
}