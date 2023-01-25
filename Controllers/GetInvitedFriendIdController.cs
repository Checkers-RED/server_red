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
            public int id;
        }

        private readonly IRedRepository _db;
        public GetInvitedFriendIdController(IRedRepository db)
        {
            _db = db;
        }
        [HttpPost(Name = "GetInvitedFriendId")]
        public IActionResult Get([FromBody] dynamic data)//(string cur_session)
        {
            CurSession curs = new CurSession();
            try
            {
                curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    output res = new output();
                    res.id = _db.GetInvitedFriendId(curs.current_session!);

                    if (res.id > 0)
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
