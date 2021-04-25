using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DeviceStateTestTask.Core.Resources;

namespace DeviceStateTestTask.ConsoleApp.Services
{
    public class TokenService
    {
        private readonly string _tokenUrl;

        public TokenService(string tokenUrl)
        {
            this._tokenUrl = tokenUrl;
        }

        public async Task<string> GetToken()
        {
            string filePath = "./token.txt";
            if (File.Exists(filePath) == false)             
            {   
                TokenResource tokenResource; 
                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, this._tokenUrl);            
                    HttpResponseMessage response = await client.SendAsync(request);
                    Stream stream = await response.Content.ReadAsStreamAsync();
                    tokenResource = await JsonSerializer.DeserializeAsync<TokenResource>(stream);                    
                }

                using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.Default))
                {
                    await sw.WriteAsync(tokenResource.Token);
                }  
            }

            using (StreamReader sr = new StreamReader(filePath))
            {                
                return await sr.ReadToEndAsync();
            }    
        }
    }
}