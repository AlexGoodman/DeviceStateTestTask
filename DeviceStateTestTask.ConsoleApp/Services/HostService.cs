using System;
using System.Threading;
using System.Threading.Tasks;
using DeviceStateTestTask.Core.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DeviceStateTestTask.ConsoleApp.Services
{
    public class HostService: IHostedService
    {        
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        public HostService(
            IHostApplicationLifetime appLifetime, 
            IConfiguration configuration,
            TokenService tokenService
        )
        {            
            this._appLifetime = appLifetime;
            this._configuration = configuration;
            this._tokenService = tokenService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._appLifetime.ApplicationStarted.Register(OnStarted);                              
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void OnStarted()
        {            
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(this._configuration["TrackHubUrl"], options => {
                    options.AccessTokenProvider = async () => await this._tokenService.GetToken();
                })
                .WithAutomaticReconnect(new TimeSpan[] {TimeSpan.Zero, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10)})
                .Build();
            
            connection.On("DeviceInfoRequest", async () =>
            {                
                Console.WriteLine("DeviceInfoRequest");
                await connection.InvokeAsync("DeviceInfoResponse", new Device {
                    ComputerName = Environment.MachineName,
                    TimeZone = System.TimeZoneInfo.Local.ToString(),
                    OsName = Environment.OSVersion.ToString(),
                    NetVersion = Environment.Version.ToString()                    
                });               
            });

            await connection.StartAsync();                    
        }        
    }
}