using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChangePassController : Controller
    {
        class input
        {
            public string? token { get; set; }
            public string? newPass { get; set; }
        }

        private readonly IRedRepository _db;
        public ChangePassController(IRedRepository db)
        {
            _db = db;
        }
        [HttpPost(Name = "ChangePass")]
        public IActionResult Get([FromBody] dynamic data)//(string token, string newPass)
        {
            try
            {
                input res = JsonSerializer.Deserialize<input>(data);
                if (res != null)
                { 
                    CurSession curs = new CurSession();

                curs.current_session = _db.ChangePass(res.token!, res.newPass!);

                if (curs.current_session != null && curs.current_session != "none" && curs.current_session.Length > 0)
                {
                    return Ok(JsonSerializer.Serialize(curs));
                    //return Ok(curs);
                }
                else
                {
                    return BadRequest();// BadRequest(400);
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

           /* string cur_session = _db.ChangePass(token, newPass);

            if (cur_session != null && cur_session.Length > 0)
            {
                return Ok(cur_session);
            }
            else
            {
                return BadRequest();
            }*/
        }
    }
}
