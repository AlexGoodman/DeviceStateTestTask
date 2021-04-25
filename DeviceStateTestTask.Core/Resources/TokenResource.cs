using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeviceStateTestTask.Core.Resources
{
    public class TokenResource
    {
        [Required]            
        [JsonPropertyName("token")]  
        public string Token {get; set;}
    }
}