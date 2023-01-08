using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SetBeatFlagController : Controller
    {
        class input
        {
            public string? current_session { get; set; }
            public bool BeatFlag { get; set; }
        }

        private readonly IRedRepository _db;
        public SetBeatFlagController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "SetBeatFlag")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                input res = JsonSerializer.Deserialize<input>(data);
                if (res != null)
                {
                    int result = _db.SetBeatFlag(res.current_session!, Convert.ToInt32(res.BeatFlag));

                    if (result == 1)
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
