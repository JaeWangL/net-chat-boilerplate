using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetChatBoilerplate.Domain.AggregatesModel.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetChatBoilerplate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepo;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IDoctorRepository doctorRepo, ILogger<WeatherForecastController> logger)
        {
            this._doctorRepo = doctorRepo ?? throw new ArgumentNullException(nameof(doctorRepo));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("test")]
        public async Task<IEnumerable<DoctorEntity>> GetTest() => await this._doctorRepo.FindAllAsync();

        [HttpPost("")]
        public async Task<DoctorEntity> CreateTest() {
            var newEntity = await this._doctorRepo.CreateAsync(new DoctorEntity("Test", "TestURl"));
            await this._doctorRepo.UnitOfWork.SaveChangesAsync();

            return newEntity;
         }
    }
}
