using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace server_red.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestConnectionController : Controller
    {
        class output
        {
            public string? field { get; set; }
        }

        [HttpGet(Name = "GetResult")]
        //public IEnumerable<WeatherForecast> Get(string nick, string pass)
        public IActionResult Get(string input)
        {
            try
            {
                output result = JsonSerializer.Deserialize<output>(input)!;
                if (result != null)
                {
                    if (result.field != null)
                    {
                        return Ok(result.field);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
