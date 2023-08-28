using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LoadBalancingTest.Controllers
{
    [Produces("application/json")]
    [Route("api/Entity")]
    //[Authorize]
    public class IoController : ControllerBase
    {
        public IoController()
        {
        }
        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            var res = await Task.Run(() => "success");
            Console.WriteLine($"----------Serving Test results");
            return Ok(res);
        }
        [HttpPost("PostTest")]
        public async Task<IActionResult> PostTest([FromBody] TestCommand command)
        {
            var res = await Task.Run(() => "success");
            Console.WriteLine($"---------Serving PostTest results");
            return Ok(res);
        }
    }

    public class TestCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
