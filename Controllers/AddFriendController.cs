using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddFriendController : Controller
    {
        class output
        {
            public string? cur_session { get; set; }
            public int f_id { get; set; }
        }

        private readonly IRedRepository _db;
        public AddFriendController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "AddFriend")]
        public IActionResult Get([FromBody] dynamic data)//(string cur_s, int fid)
        {
            try
            {
                output res = JsonSerializer.Deserialize<output>(data);
                if (res != null)
                {
                    int result = _db.AddFriend(res.cur_session!, res.f_id);

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


            /*int res = _db.AddFriend(cur_s, fid);

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
