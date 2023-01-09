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
    public class RejectMatchController : Controller
    {
        class input
        {
            public string? current_session { get; set; }
            public string org_nick { get; set; }
        }
        private readonly IRedRepository _db;
        public RejectMatchController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "RejectMatch")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                input inres = JsonSerializer.Deserialize<CurSession>(data);
                if (inres != null)
                {
                    int res = _db.RejectMatch(inres.current_session!, inres.org_nick);

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