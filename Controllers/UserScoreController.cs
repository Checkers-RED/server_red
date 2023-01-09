using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserScoreController : Controller
    {
        private readonly IRedRepository _db;
        public UserScoreController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "UserScore")]
        public IActionResult Get([FromBody] dynamic data)//(string cur_s)
        {
            try
            {
                CurSession curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    UserScore u = _db.GetUserScore(curs.current_session);
                    if (u.nick != null) 
                    {
                        return Ok(JsonSerializer.Serialize(u));
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
            /*UserScore u = _db.GetUserScore(cur_s);

            if (u.nick != null)
            {
                return Ok(u);
            }
            else
            {
                return BadRequest();
            }*/
        }
    }
}
