using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DeviceStateTestTask.Core.IServices;
using DeviceStateTestTask.Core.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DeviceStateTestTask.AzureFunctions
{
    public class SaveDeviceStateChanges
    {
        private readonly IDeviceService _deviceService;

        public SaveDeviceStateChanges(IDeviceService deviceService)
        {
            this._deviceService = deviceService;
        }

        [Function("SaveDeviceStateChanges")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                string requestBody = await streamReader.ReadToEndAsync();
                Device device = JsonConvert.DeserializeObject<Device>(requestBody);                         
            
                HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json; charset=utf-8");        
                response.WriteString(JsonConvert.SerializeObject(new {
                    IsUpdated = this._deviceService.SaveChanges(device)
                })); 
                return response;                          
            }                               
        }
    }
}
