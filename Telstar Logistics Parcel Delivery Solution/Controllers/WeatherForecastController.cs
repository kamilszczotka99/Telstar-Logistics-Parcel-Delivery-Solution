using Microsoft.AspNetCore.Mvc;
using Telstar_Logistics_Parcel_Delivery_Solution.Calculations;
using System.ComponentModel;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "a", "b", "c", "Cool", "d", "e", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WeatherForecastController> _logger;

        private RouteMapperService service;
        
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context, RouteMapperService _service)
        {
            this.service = _service;

            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
        
       
            List<(int, int, int)> cityMap = new List<(int, int, int)>()
            {
                (11, 9, 4),
                (11, 8, 6),
                (11, 14, 3),
                (10, 7, 5),
                (10, 8, 3),
                //(10, 7, 0),
                //(10, 9, 0),
                //(9, 10, 0),
                //(9, 28, 0),
                //(9, 28, -1),
                (9, 11, 4),
                (8, 10, 3),
                (8, 11, 6),
                //(8, 4, -1),
                //(8, 7, -1),
                //(8, 14, -1),
                (7, 10, 5),
                (7, 10, 0),
                (7, 6, 5),
                (7, 1, 2),
                //(7, 8, -1),
                //(7, 29, 0),
                (6, 14, 8),
                (14, 6, 8),
                (6, 7, 5),
                //(1, 7, -1),
                (1, 7, 2),
                //(1, 4, -1),
                //(1, 3, -1),
                (1, 2, 8),
                (2, 1, 8),
                (14, 25, 2),
                (25, 14, 2),
                (14, 11, 3),
                (14, 28, 4),
                (28, 14, 4)
                //(14, 28, -1),
                //(14, 8, -1),
                //(14, 24, -1)
            };

            service.Execute(cityMap, 11, 6).ForEach(p => Console.WriteLine(p));
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}