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
    public class RevokeMatchController : Controller
    {
        class input
        {
            public string? current_session { get; set; }
            public int f_id { get; set; }
        }
        private readonly IRedRepository _db;
        public RevokeMatchController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "RevokeMatch")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                input inres = JsonSerializer.Deserialize<CurSession>(data);
                if (inres != null)
                {
                    int res = _db.RevokeMatch(inres.current_session!, inres.f_id);

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