using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EndMatchController : Controller
    {
        class input
        {
            public string? current_session { get; set; }
            public string? color_win { get; set; }
        }

        private readonly IRedRepository _db;
        public EndMatchController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "EndMatch")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                input inres = JsonSerializer.Deserialize<input>(data);
                if (inres != null)
                {
                    int res = _db.EndMatch(inres.current_session!, inres.color_win!);

                    if (res == 1)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}