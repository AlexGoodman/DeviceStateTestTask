using Microsoft.Extensions.Hosting;
using DeviceStateTestTask.Data;
using Microsoft.Extensions.DependencyInjection;
using DeviceStateTestTask.Data.IRepositories;
using DeviceStateTestTask.Services;
using DeviceStateTestTask.Data.Repositories;
using System;
using DeviceStateTestTask.Core.IServices;

namespace DeviceStateTestTask.AzureFunctions
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureServices(services => {                
                    services.AddScoped<DataConnection>(serviceProvider => new DataConnection(
                        Environment.GetEnvironmentVariable("DbProviderName"),                 
                        Environment.GetEnvironmentVariable("DbConnectionString")
                    ));
                    
                    services.AddScoped<IDeviceRepository, DeviceRepository>();
                    services.AddScoped<IDeviceService, DeviceService>();                   
                })
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}