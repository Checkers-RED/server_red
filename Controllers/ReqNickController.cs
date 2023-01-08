using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReqNickController : Controller
    {
        class input
        {
            public string? nick { get; set; }
        }

        private readonly IRedRepository _db;
        public ReqNickController(IRedRepository db)
        {
            _db = db;
        }
        [HttpPost(Name = "GetRnick")]
        public IActionResult Get([FromBody] dynamic data)//(string nick)
        {
            ReqNick r = new ReqNick();
            try
            {
                input res = JsonSerializer.Deserialize<input>(data);
                if (res != null)
                {
                    r = _db.ReqNick(res.nick);
                    if (r != null)
                    {
                        return Ok(JsonSerializer.Serialize(r));
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

            /*List<string> res = _db.ReqNick(nick);
            ReqNick r = new ReqNick();

            if (res.Count > 0)
            {
                r.token = res[0];
                r.question = res[1];
                return Ok(r);
            }
            else
            {
                return BadRequest();
            }*/
        }
    }
}
