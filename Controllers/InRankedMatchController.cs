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
    public class InRankedMatchController : Controller
    {
        class input
        {
            public string? current_session { get; set; }
            public int rules_id { get; set; }
        }

        private readonly IRedRepository _db;
        public InRankedMatchController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "InRankedMatch")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                input inres = JsonSerializer.Deserialize<input>(data);
                if (inres != null)
                {
                    int res = _db.InRankedMatch(inres.current_session!, inres.rules_id);

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
