using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Business.Common.JWTToken
{
    public class JwtTokenGenerationHelper
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Generate User Token
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<string> GenerateUJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userName", userName) }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }

        /// <summary>
        /// Generate Mac Token
        /// </summary>
        /// <param name="macAddress"></param>
        /// <param name="uToken"></param>
        /// <returns></returns>
        public Task<string> GenerateMacJwtToken(string macAddress, string uToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("macAddress", macAddress) }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }

        /// <summary>
        /// Generate Application Token
        /// </summary>
        /// <param name="macToken"></param>
        /// <param name="uToken"></param>
        /// <returns></returns>
        public Task<string> GenerateAppJwtToken(string macToken, string uToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("macToken", macToken) }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }

    }
}
