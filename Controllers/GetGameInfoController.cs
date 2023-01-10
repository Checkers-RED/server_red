using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetGameInfoController : Controller
    {
        private readonly IRedRepository _db;
        public GetGameInfoController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "GetGameInfo")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                CurSession curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    GameInfo g = _db.GetGameInfo(curs.current_session!);
                    if (g != null && g.nick1 != null && g.nick1 != "")
                    {
                        return Ok(JsonSerializer.Serialize(g));
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