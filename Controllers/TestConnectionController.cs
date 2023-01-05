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
        public IActionResult Get(string data)
        {
            try
            {
                output result = JsonSerializer.Deserialize<output>(data)!;
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
