using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SetActiveColorController : Controller
    {
        class input
        {
            public string? current_session { get; set; }
            public string? active_color { get; set; }
        }

        private readonly IRedRepository _db;
        public SetActiveColorController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "SetActiveColor")]
        public IActionResult Get([FromBody] dynamic data)//(string cur_s, int fid)
        {
            try
            {
                input res = JsonSerializer.Deserialize<input>(data);
                if (res != null)
                {
                    int result = _db.SetActiveColor(res.current_session!, res.active_color!);

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
