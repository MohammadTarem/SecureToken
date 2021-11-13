using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecureToken;
using SecureToken.Authorization;

namespace AuthWithSecureToken.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SecureTokenOptions TokenOptions;

        public WeatherForecastController(SecureTokenOptions tokenOption, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            TokenOptions = tokenOption;
        }

        [HttpGet("issue/{name}")]
        public  IActionResult IssueToken(string name)
        {

            var claims = new KeyValuePair<string, string>[]
            {   
                new KeyValuePair<string, string>(ClaimTypes.Role, "Admin")
            };


            var token = AuthorizationToken.Issue(name, claims, DateTime.Now, TimeSpan.FromSeconds(300), TokenOptions);
            return Ok(new { Token =  token });

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation(HttpContext.User.Identity.Name);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
