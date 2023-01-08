using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GiveUpController : Controller
    {

        private readonly IRedRepository _db;
        public GiveUpController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "GiveUp")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                CurSession curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    int res = _db.GiveUp(curs.current_session!);

                    if (res == 1)
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
