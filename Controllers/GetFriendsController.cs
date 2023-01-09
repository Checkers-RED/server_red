using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetFriendsController : Controller
    {
        class output
        {
            public List<User> friendlist= new List<User>();
        }

        private readonly IRedRepository _db;
        public GetFriendsController(IRedRepository db)
        {
            _db = db;
        }
        [HttpPost(Name = "GetFriends")]
        public IActionResult Get([FromBody] dynamic data)//(string cur_session)
        {
            CurSession curs = new CurSession();
            try
            {
                curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    output result = new output();
                    result.friendlist = _db.GetFriendlist(curs.current_session!);
                    //if (result.friendlist.Count > 0)
                    //{
                        return Ok(JsonSerializer.Serialize(result.friendlist));
                    //}
                   // else
                    //{
                    //    return BadRequest();
                    //}
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

           /* List<User> res = _db.GetFriendlist(cur_session);

            if (res.Count > 0)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }*/
        }
    }
}
