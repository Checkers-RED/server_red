using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchUserController : Controller
    {
        private readonly IRedRepository _db;
        public SearchUserController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "SearchUser")]
        public IActionResult Get([FromBody] dynamic data)//(string username)
        { // входные данные: nick
            try
            {
                User res = JsonSerializer.Deserialize<User>(data);
                if (res != null)
                {
                    User u = _db.UserSearch(res.nick);
                    if (u.uid > 0)
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

            /*_db.UserSearch(username);

            if (u.uid > 0)
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
