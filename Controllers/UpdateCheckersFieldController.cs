using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UpdateCheckersFieldController : Controller
    {
        class input
        {
            public string? cur_session { get; set; }
            public List<Checker> white = new List<Checker>();
            public List<Checker> black = new List<Checker>();
        }

        private readonly IRedRepository _db;
        public UpdateCheckersFieldController(IRedRepository db)
        {
            _db = db;
        }
        [HttpPost(Name = "UpdateCheckersField" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                input inres = JsonSerializer.Deserialize<CurSession>(data);
                if (inres != null)
                {
                    for(int i = 0; i < inres.white.Count; i++)
                    {
                        bool delete_old = false; // нужно ли удалять все шашки текущего матча (удаляются при занесении 1й шашки)
                        if (i == 0)
                        {
                            delete_old = true;
                        }
                        int res = _db.UpdateCheckersField(inres.cur_session!, inres.white[i].color!, inres.white[i].horiz, inres.white[0].vertic, Convert.ToInt32(inres.white[0].isQueen), Convert.ToInt32(inres.white[0].isBeaten), delete_old);
                        if (res == 0) // с какой-то из шашек проблема. тут потеряются все шашки, которые были до удаления (начала обновления), а новые до конца не проставятся -_-
                        {
                            return BadRequest();
                        }
                    }
                    for (int i = 0; i < inres.black.Count; i++)
                    {
                        bool delete_old = false;
                        int res = _db.UpdateCheckersField(inres.cur_session!, inres.white[i].color!, inres.white[i].horiz, inres.white[0].vertic, Convert.ToInt32(inres.white[0].isQueen), Convert.ToInt32(inres.white[0].isBeaten), delete_old);
                        if (res == 0) // с какой-то из шашек проблема. тут потеряются все шашки, которые были до удаления (начала обновления), а новые до конца не проставятся -_-
                        {
                            return BadRequest();
                        }
                    }
                    return Ok();
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
