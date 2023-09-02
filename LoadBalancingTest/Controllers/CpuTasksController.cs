using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoadBalancingTest.Controllers;

[Produces("application/json")]
[Route("[controller]")]
public class CpuTasksController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private PerformanceCounter _cpuCounter;

    public CpuTasksController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        //_cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        //_cpuCounter.NextValue();
    }
    [HttpGet("GetCpu/{scale}")]
    public async Task<IActionResult> DoCpuTask(int scale)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < scale * 100; i++)
        {
            var sqrt = Math.Sqrt(123123413);
            var gcd = Math.BigMul(111111, 111111);
            var sqrt1 = Math.Sqrt(123123413);
            var gcd1 = Math.BigMul(111111, 111111);
            var sqrt2 = Math.Sqrt(123123413);
            var gcd2 = Math.BigMul(111111, 111111);
        }
        stopwatch.Stop();
        long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;

        // Print the results
        Console.WriteLine($"Elapsed time in milliseconds: {elapsedMilliseconds} ms");
        Console.WriteLine($"Elapsed time in seconds: {elapsedSeconds} s");
        await Task.Run(() => { });
        return Ok("s");
    }
    [HttpPost("DoCpu")]
    public async Task<IActionResult> PostCpuTask([FromBody] TaskConfigCommand command)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < command.Outer * 10000000; i++)
        {
            for(int j=0; j<command.Inner * 5; j++)
            {
                var sqrt = Math.Sqrt(123123413);
                var gcd = Math.BigMul(111111, 111111);
                var sqrt1 = Math.Sqrt(123123413);
                var gcd1 = Math.BigMul(111111, 111111);
                var sqrt2 = Math.Sqrt(123123413);
                var gcd2 = Math.BigMul(111111, 111111);
            }
        }
        stopwatch.Stop();
        long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;

        // Print the results
        Console.WriteLine($"Elapsed time in milliseconds: {elapsedMilliseconds} ms");
        Console.WriteLine($"Elapsed time in seconds: {elapsedSeconds} s");
        await Task.Run(() => { });
        return Ok("s");
    }
    [HttpGet("consume/{percentage}")]
    public async Task<IActionResult> ConsumeCpu(int percentage)
    {
        if (percentage <= 0 || percentage > 100)
        {
            return BadRequest("Percentage should be between 1 and 100.");
        }

        try
        {
            // Calculate the number of CPU-bound tasks based on the desired percentage.
            int numberOfTasks = Environment.ProcessorCount * percentage / 100;

            // Start CPU-bound tasks to consume resources.
            var tasks = new Task[numberOfTasks];
            for (int i = 0; i < numberOfTasks; i++)
            {
                tasks[i] = Task.Run(() => ConsumeCpuTask());
            }

            // Wait for all tasks to complete.
            await Task.WhenAll(tasks);


            return Ok($"Consumed CPU at {percentage}%");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
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
public class TaskConfigCommand
{
    public int Outer { get; set; }
    public int Inner { get; set; }
}

