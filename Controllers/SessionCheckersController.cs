using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SessionCheckersController : Controller
    {
        class output
        {
            public List<Checker> white = new List<Checker>();
            public List<Checker> black = new List<Checker>();
        }

        private readonly IRedRepository _db;
        public SessionCheckersController(IRedRepository db)
        {
            _db = db;
        }
        [HttpPost(Name = "SessionCheckers")]
        public IActionResult Get([FromBody] dynamic data)//(string cur_session)
        {
            CurSession curs = new CurSession();
            try
            {
                curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    output res = new output();
                    res.white = _db.SessionCheckersWhite(curs.current_session!);
                    res.black = _db.SessionCheckersBlack(curs.current_session!);

                    if (res.white.Count > 0 && res.black.Count > 0)
                    {
                        return Ok(JsonSerializer.Serialize(res));
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
