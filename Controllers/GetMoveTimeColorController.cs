using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetMoveTimeColorController : Controller
    {
        private readonly IRedRepository _db;
        public GetMoveTimeColorController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "GetMoveTimeColor")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                CurSession curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    ActMoveTimeColor m = _db.GetActMoveTimeColor(curs.current_session!);
                    if (m != null && m.color != null && m.color != "")
                    {
                        return Ok(JsonSerializer.Serialize(m));
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