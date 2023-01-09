using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetNotifsController : ControllerBase
    {
        class output
        {
            public List<Notif> notifslist = new List<Notif>();
        }

        private readonly IRedRepository _db;
        public GetNotifsController(IRedRepository db)
        {
            _db = db;
        }
        [HttpPost(Name = "GetNotifs")]
        public IActionResult Get([FromBody] dynamic data)//(string cur_session)
        {
            CurSession curs = new CurSession();
            try
            {
                curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    output result = new output();
                    result.notifslist = _db.GetNotiflist(curs.current_session!);
                    //if (result.notifslist.Count > 0)
                    //{
                        return Ok(JsonSerializer.Serialize(result.notifslist));
                    //}
                    //else
                   // {
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

            /*List<Notif> res = _db.GetNotiflist(cur_session);

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
