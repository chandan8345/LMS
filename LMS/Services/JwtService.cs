using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public string GenerateToken(string userId, string role)
        //{
        //    var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtService>();
        //    var claims = new[]
        //    {
        //    new Claim(JwtRegisteredClaimNames.Sub, userId),
        //    new Claim(ClaimTypes.Role, role)
        //};

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: jwtSettings.Issuer,
        //        audience: jwtSettings.Audience,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpiresInMinutes),
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
