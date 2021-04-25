using System.IdentityModel.Tokens.Jwt;
using DeviceStateTestTask.Core.Resources;
using DeviceStateTestTask.WebService.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DeviceStateTestTask.WebService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TokenController: ControllerBase
    {
        [HttpGet]                
        public IActionResult Get() => Ok(new TokenResource {
            Token =  new JwtSecurityTokenHandler().WriteToken(JwtHelper.CreateJwt())
        });          
    }
}