using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DeviceStateTestTask.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace DeviceStateTestTask.WebService.Hubs
{
    
    [Authorize]
    public class TrackHub: Hub
    {  
        public const string SEND_MESSAGE_TYPE = "DeviceInfoRequest";
        private readonly IConfiguration _configuration;

        public TrackHub(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task DeviceInfoResponse(Device device)
        {   
            device.Id = this.GetDeviceId();
            device.IsOnline = true;                
            await this.SaveDeviceChanges(device);                            
        }

        public override async Task OnConnectedAsync()
        {            
            await this.Clients.Caller.SendAsync(TrackHub.SEND_MESSAGE_TYPE);                                                   
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {                        
            await this.SaveDeviceChanges(new Device {Id = this.GetDeviceId(), IsOnline = false});
            await base.OnDisconnectedAsync(exception);
        }

        private string GetDeviceId()
        {
            ClaimsIdentity identity = Context.User.Identity as ClaimsIdentity;
            return identity.FindFirst("uid").Value; 
        }

        private async Task SaveDeviceChanges(Device device)
        {
            using (HttpClient client = new HttpClient())
            {                        
                HttpResponseMessage response = await client.PostAsync(
                    this._configuration["SaveDeviceChangesUrl"], 
                    new StringContent(
                        JsonSerializer.Serialize(device),
                        Encoding.UTF8,
                        "application/json"
                    )
                );                 ;                    
            } 
        }    
    }
}