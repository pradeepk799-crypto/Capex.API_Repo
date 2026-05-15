using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Capex.Business.Common.JWTToken;
using Capex.Models.Common;
using System.Net.NetworkInformation;

namespace Capex.API.Areas.Common.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTTokenGenrationController : ControllerBase
    {
        private readonly JwtTokenGenerationHelper _jwtTokenHelper;
        private readonly JwtTokenValidiateHelper _validiateJwtHelper;
        public JWTTokenGenrationController(JwtTokenGenerationHelper jwtTokenHelper, JwtTokenValidiateHelper validiateJwtHelper)
        {
            _jwtTokenHelper = jwtTokenHelper;
            _validiateJwtHelper = validiateJwtHelper;
        }

        [Route("GenerateUToken")]
        [HttpPost]
        public async Task<IActionResult> GenerateUToken(string UserId, string Password)
        {

            if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(Password))
            {
                var user = GetUser(UserId, Password);

                if (user != null)
                {
                    var token = await _jwtTokenHelper.GenerateUJwtToken(UserId);
                    var result = new JWTTokenResponseEntity.ResponseModel
                    {
                        Status = "Success",
                        StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK,
                        SuccessMessage = token,
                    };

                    return Ok(result);

                    //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    var result = new JWTTokenResponseEntity.ResponseModel
                    {
                        Status = "Success",
                        StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest,
                        ErrorMessage = "Invalid credentials",
                    };
                    return BadRequest(result);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("GenerateMacToken")]
        [HttpPost]
        public async Task<IActionResult> GenerateMacUToken(string UToken, string MacAddress)
        {
            var isValidateUserToken = _validiateJwtHelper.ValidateUToken(UToken);
            if (!string.IsNullOrEmpty(isValidateUserToken))
            {
                var _MacAddress = GetMACAddress();
                if (!string.IsNullOrEmpty(_MacAddress) && !string.IsNullOrEmpty(UToken))
                {
                    string token = await _jwtTokenHelper.GenerateMacJwtToken(_MacAddress, UToken);
                    var result = new JWTTokenResponseEntity.ResponseModel
                    {
                        Status = "Success",
                        StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK,
                        SuccessMessage = token,
                    };

                    return Ok(result);

                    //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    var result = new JWTTokenResponseEntity.ResponseModel
                    {
                        Status = "Failed",
                        StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized,
                        ErrorMessage = "Invalid credentials",
                    };
                    return BadRequest(result);
                }
            }
            else
            {
                var result = new JWTTokenResponseEntity.ResponseModel
                {
                    Status = "Failed",
                    StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid Token",
                };
                return BadRequest(result);
            }

        }

        [Route("GenerateAppUToken")]
        [HttpPost]
        public async Task<IActionResult> GenerateAppUToken(string UToken, string MacToken)
        {
            if (!string.IsNullOrEmpty(UToken) && !string.IsNullOrEmpty(MacToken))
            {
                var isValidateUserToken = _validiateJwtHelper.ValidateUToken(UToken);
                var isValidateMacToken = _validiateJwtHelper.ValidateMacToken(MacToken);
                if (!string.IsNullOrEmpty(isValidateUserToken))
                {
                    if (!string.IsNullOrEmpty(isValidateMacToken))
                    {
                        string token = await _jwtTokenHelper.GenerateAppJwtToken(UToken, MacToken);
                        var result = new JWTTokenResponseEntity.ResponseModel
                        {
                            Status = "Success",
                            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK,
                            SuccessMessage = token,
                        };

                        return Ok(result);

                        //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                    else
                    {
                        var result = new JWTTokenResponseEntity.ResponseModel
                        {
                            Status = "Failed",
                            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest,
                            ErrorMessage = "Invalid mac token",
                        };
                        return BadRequest(result);
                    }
                }
                else
                {
                    var result = new JWTTokenResponseEntity.ResponseModel
                    {
                        Status = "Failed",
                        StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest,
                        ErrorMessage = "Invalid user token",
                    };
                    return BadRequest(result);
                }
            }
            else
            {
                var result = new JWTTokenResponseEntity.ResponseModel
                {
                    Status = "Failed",
                    StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest,
                    ErrorMessage = "Enter user token and mac token",
                };
                return BadRequest(result);
            }
        }

        private List<JWTTokenResponseEntity.Users> GetUser(string email, string password)
        {
            List<JWTTokenResponseEntity.Users> users = new List<JWTTokenResponseEntity.Users>();
            JWTTokenResponseEntity.Users objUsers = new JWTTokenResponseEntity.Users();
            objUsers.Email = "vipul@gmail.com";
            objUsers.Password = "111";
            users.Add(objUsers);
            return users;
        }

        private string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

    }
}
