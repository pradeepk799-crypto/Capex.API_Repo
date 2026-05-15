using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Capex.Models.Common;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Capex.Business.Common
{
    public static class TokenAuth
    {
        /// <summary>
        /// Get Private Key
        /// </summary>
        /// <returns></returns>
        public static SigningCredentials GetJwtPrivateKey(RSA rsa)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            string issuer = config["JwtSettings:Issuer"];
            string audience = config["JwtSettings:Audience"];
            string secretKey = config["JwtSettings:SecretKey"];



            SigningCredentials signingCredentials = null;


            
                var key = Encoding.ASCII.GetBytes(AppSettings.Current.Secret);
                signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            

            return signingCredentials;
        }
        /// <summary>
        ///  GetJwtPublicKey
        /// </summary>
        /// <returns></returns>
        private static SecurityKey GetJwtPublicKey(object kid)
        {
            SecurityKey issuerSigningKey = null;

            try
            {
                
                    var key = Encoding.ASCII.GetBytes(AppSettings.Current.Secret);
                    issuerSigningKey = new SymmetricSecurityKey(key);
                
            }
            catch (Exception ex)
            {
                Log.Error("Authentication Error ", ex);
            }

            return issuerSigningKey;
        }

        /// <summary>
        /// GetRefreshTokenValidationParameters
        /// </summary>
        /// <param name="token"></param>
        /// <param name="validatedToken"></param>
        /// <returns></returns>
        public static ClaimsPrincipal ValidateToken(string token, out SecurityToken validatedToken)
        {
            //Extract kid from token
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            string kid = jwtSecurityToken.Header.Kid;

            var validationParameters = GetTokenValidationParameters(kid);
            var principal = new JwtSecurityTokenHandler();
            var result = principal.ValidateToken(token, validationParameters, out validatedToken);
            return result;
        }

        /// <summary>
        /// GetTokenValidationParameters
        /// </summary>
        /// <returns></returns>
        public static TokenValidationParameters GetTokenValidationParameters(string inputKid = null)
        {
            var tokenParameter = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                //TokenDecryptionKey = new SymmetricSecurityKey(claimKey), //payload encrpt 
                // Set issuerSigningKey as per your logic.
                // This delegate will be executed for eahc request.
                IssuerSigningKeyResolver = (token, secutiryToken, kid, validationParameters) =>
                {
                    if (!string.IsNullOrEmpty(inputKid))
                    {
                        SecurityKey issuerSigningKey = TokenAuth.GetJwtPublicKey(inputKid);
                        return new List<SecurityKey>() { issuerSigningKey };
                    }
                    else
                    {
                        SecurityKey issuerSigningKey = TokenAuth.GetJwtPublicKey(kid);
                        return new List<SecurityKey>() { issuerSigningKey };
                    }

                }
            };

            return tokenParameter;
        }

    }
}
