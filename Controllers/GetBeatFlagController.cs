using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;


namespace server_red.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetBeatFlagController : Controller
    {
        class output
        {
            public bool beat_flag { get; set; }
        }

        private readonly IRedRepository _db;
        public GetBeatFlagController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "GetBeatFlag")]
        public IActionResult Get([FromBody] dynamic data)

        {
            try
            {
                CurSession curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    output res = new output();

                    int r = _db.GetBeatFlag(curs.current_session!);
                    if (r == 1)
                    {
                        res.beat_flag = true;
                        return Ok(JsonSerializer.Serialize(res));
                    }
                    else if (r == 0)
                    {
                        res.beat_flag = false;
                        return Ok(JsonSerializer.Serialize(res));
                    }
                    else // if r == -1
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
