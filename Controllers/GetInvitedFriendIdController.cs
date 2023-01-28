using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetInvitedFriendIdController : Controller
    {
        class output
        {
            public int f_id { get; set; }
        }

        private readonly IRedRepository _db;
        public GetInvitedFriendIdController(IRedRepository db)
        {
            _db = db;
        }
        [HttpPost(Name = "GetInvitedFriendId")]
        public IActionResult Get([FromBody] dynamic data)//(string cur_session)
        {
            //CurSession curs = new CurSession();
            try
            {
                CurSession curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    output res = new()
                    {
                        f_id = _db.GetInvitedFriendId(curs.current_session!)
                    };
                    //res.id = _db.GetInvitedFriendId(curs.current_session!);

                    if (res.f_id > 0)
                    {
                        return Ok(JsonSerializer.Serialize(res));
                    }
                    else
                    {
                        return Ok(JsonSerializer.Serialize(10));
                        //return BadRequest();
                    }
                }
                else
                {
                    return Ok(JsonSerializer.Serialize(20));
                    //return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return Ok(JsonSerializer.Serialize(30));
                //return BadRequest();
            }
        }
    }
}
