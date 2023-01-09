using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InviteFriendController : Controller
    {
        class input
        {
            public string? current_session { get; set; }
            public int f_id { get; set; }
            public int move_time { get; set; }
            public int rules_id { get; set; }
        }

        private readonly IRedRepository _db;
        public InviteFriendController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "InviteFriend")]
        public IActionResult Get([FromBody] dynamic data)//(string cur_s, int fid)
        {
            try
            {
                input res = JsonSerializer.Deserialize<input>(data);
                if (res != null)
                {
                    int result = _db.InviteFriend(res.current_session!, res.f_id, res.move_time, res.rules_id);

                    if (result == 1)
                    {
                        return Ok();
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
