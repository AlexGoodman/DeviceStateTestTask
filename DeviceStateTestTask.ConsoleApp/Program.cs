using System.Threading.Tasks;
using DeviceStateTestTask.ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DeviceStateTestTask.ConsoleApp
{
    class Program
    {                  
        static async Task Main(string[] args)
        { 
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {                                
                    services.AddHostedService<HostService>();                                        
                    services.AddSingleton<TokenService>(provider => new TokenService(hostContext.Configuration["TokenUrl"]));                                        
                })
                .Build();                        

            await host.RunAsync();                                
        }
    }
}
