using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IsInRankedMatchController : Controller
    {
        class output
        {
            public bool status { get; set; }
        }
        private readonly IRedRepository _db;
        public IsInRankedMatchController(IRedRepository db)
        {
            _db = db;
        }

        [HttpPost(Name = "IsInRankedMatch")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                CurSession curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    int inMatch = _db.IsInRankedMatch(curs.current_session!);
                    if (inMatch == 2)
                    {
                        output res = new()
                        {
                            status = false
                        };
                        return Ok(res);
                    }
                    else if (inMatch == 1)
                    {
                        output res = new()
                        {
                            status = true
                        };
                        return Ok(res);
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
