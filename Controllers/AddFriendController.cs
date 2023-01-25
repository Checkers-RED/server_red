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
        class input
        {
            public string? current_session { get; set; }
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
                input inres = JsonSerializer.Deserialize<input>(data);
                if (inres != null)
                {
                    int result = _db.AddFriend(inres.current_session!, inres.f_id);

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
