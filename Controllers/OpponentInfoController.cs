using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OpponentInfoController : Controller
    {
        private readonly IRedRepository _db;
        public OpponentInfoController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "OpponentInfo")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                CurSession curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    Opponent o = _db.GetOpponentInfo(curs.current_session!);
                    if (o.nick != null)
                    {
                        return Ok(JsonSerializer.Serialize(o));
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