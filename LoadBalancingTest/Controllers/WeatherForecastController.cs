using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoadBalancingTest.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private PerformanceCounter _cpuCounter;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        //_cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        //_cpuCounter.NextValue();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    private void ConsumeCpuTask()
    {
        // This method consumes CPU resources by running a busy-waiting loop.
        // Adjust the complexity of the loop to control the CPU usage.

        while (true)
        {
            // Adjust the workload to increase or decrease CPU usage.
            // This is a simple example; you can make the workload more complex.
            for (int i = 0; i < 1000000; i++)
            {
                Math.Sqrt(i);
            }
        }
    }
}

