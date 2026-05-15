using Microsoft.AspNetCore.Mvc;
using Capex.Business.Interfaces;

namespace Capex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly IBusinessServices businessServices;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IBusinessServices businessServices, ILogger<WeatherForecastController> logger)
        {
            this.businessServices = businessServices;
            this._logger = logger;
        }

        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            this._logger.LogInformation("Test");
            this.businessServices.User.GetName();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("Test")]
        public async Task<string> Test()
        {
            this._logger.LogInformation("Test");
            string item = "";
            return item;
        }

        [HttpGet("ErrorTest")]
        public async Task<string> ErrorTest()
        {
            try
            {
                int a = 5; int b = 0;
                int c = a / b;
                return "Yogesh";
            }
            catch (Exception ex)
            {
                businessServices.DBLogger.AddErrorLog(ex.ToString());
                return "Error";
            }

        }

    }
}