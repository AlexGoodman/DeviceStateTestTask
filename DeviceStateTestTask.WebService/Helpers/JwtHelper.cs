using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DeviceStateTestTask.WebService.Helpers
{
    public class JwtHelper
    {
        public const string ISSUER = "WebService";
        public const string AUDIENCE = "WebService";
        private const string KEY = "Custom123Super456Secret789Key";
        public const int LIFETIME = 0;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        public static JwtSecurityToken CreateJwt(IEnumerable<Claim> claims = null)
        {                        
            claims = claims ?? 
                    new List<Claim>{new Claim("uid", System.Guid.NewGuid().ToString())};
                        
            var now = DateTime.UtcNow;
            return new JwtSecurityToken(            
                issuer: ISSUER,
                audience: AUDIENCE,
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(LIFETIME)),
                signingCredentials: new SigningCredentials(
                    GetSymmetricSecurityKey(), 
                    SecurityAlgorithms.HmacSha256
                )
            );        
        }
    }
}