using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeleteFriendController : Controller
    {
        class input
        {
            public string? cur_session { get; set; }
            public int f_id { get; set; }
        }

        private readonly IRedRepository _db;
        public DeleteFriendController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "DeleteFriend")]
        public IActionResult Get([FromBody] dynamic data)//(string cur_s, int fid)
        {
            try
            {
                input res = JsonSerializer.Deserialize<input>(data);
                if (res != null)
                {
                    int result = _db.DeleteFriend(res.cur_session!, res.f_id);

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


            /*int res = _db.DeleteFriend(cur_s, fid);

            if (res == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }*/
        }
    }
}
