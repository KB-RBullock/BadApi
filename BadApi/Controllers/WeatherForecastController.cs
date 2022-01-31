using BadApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace BadApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherDbContext ctx;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherDbContext ctx)
        {
            _logger = logger;
            this.ctx = ctx;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return ctx.Forecasts;
        }

        [HttpPost]
        public async void Post(WeatherForecast newForecast)
        {
            ctx.Forecasts.Add(newForecast);
            ctx.SaveChangesAsync();
        }

        [HttpPut]
        public async void Put(WeatherForecast newForecast)
        {
            var forecast = ctx.Forecasts.First(m=>m.Id == newForecast.Id);
            forecast.Date = newForecast.Date;
            forecast.Summary = newForecast.Summary;
            forecast.TemperatureC = newForecast.TemperatureC;
            ctx.SaveChanges();
        }

    }
}