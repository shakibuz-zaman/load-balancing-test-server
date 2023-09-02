using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using LoadBalancingTest.Services;
using LoadBalancingTest.Models;
using System.Diagnostics;
using System.Net;

namespace LoadBalancingTest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize]
    public class IoController : ControllerBase
    {
        private readonly IDemoService _demoService;
        public IoController(IDemoService demoService)
        {
            _demoService = demoService;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Test(int cnt)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //
            var users = await _demoService.GetFirstXUserInfo(cnt);
            Console.WriteLine($"----------Serving Test results");
            //
            double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine($"Elapsed time in seconds: {elapsedSeconds} s");
            return Ok(elapsedSeconds);
        }
        [HttpPost("Post")]
        public async Task<IActionResult> PostTest([FromBody] TestCommand command)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            await _demoService.ReadWriteUserInfo(command.ReadCnt, command.WriteCnt);

            double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
            //var response = new HttpResponseMessage(HttpStatusCode.OK);

            // Set the Transfer-Encoding header to "chunked"
            //response.Headers.TransferEncodingChunked = true;

            Console.WriteLine($"Elapsed time in seconds: {elapsedSeconds} s");
            return Ok(elapsedSeconds);
        }
        [HttpPost("PostAdd")]
        public async Task<IActionResult> PostAddTest([FromBody] CreateCommand command)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            await _demoService.SaveUserInfo(command.ItemsCount);

            double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
            //var response = new HttpResponseMessage(HttpStatusCode.OK);

            // Set the Transfer-Encoding header to "chunked"
            //response.Headers.TransferEncodingChunked = true;

            Console.WriteLine($"Elapsed time in seconds: {elapsedSeconds} s");
            return Ok(elapsedSeconds);
        }
    }

    public class TestCommand
    {
        public int ReadCnt { get; set; }
        public int WriteCnt { get; set; }

    }
    public class CreateCommand
    {
        public int ItemsCount { get; set; }

    }
}
