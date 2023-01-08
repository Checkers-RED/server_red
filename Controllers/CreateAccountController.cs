using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CreateAccountController : Controller
    {
        class input
        {
            public string? nick { get; set; }
            public string? pass { get; set; }
            public string? ques { get; set; }
            public string? ans { get; set; }
        }

        private readonly IRedRepository _db;
        public CreateAccountController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "GetAccount")]
        public IActionResult Get([FromBody] dynamic data)//(string nick, string pass, string ques, string ans)
        {
            try
            {
                input res = JsonSerializer.Deserialize<input>(data);
                if (res != null)
                {
                    CurSession curs = new CurSession();

                    curs.current_session = _db.CreateAccount(res.nick!, res.pass!, res.ques!, res.ans!);

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


            /*string cur_session = _db.CreateAccount(nick, pass, ques, ans);

            if (cur_session != null && cur_session != "none" && cur_session.Length > 0)
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