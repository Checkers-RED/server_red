using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;

namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetMovesController : Controller
    {
        class output
        {
            public List<Move> moveslist = new List<Move>();
        }

        private readonly IRedRepository _db;
        public GetMovesController(IRedRepository db)
        {
            _db = db;
        }
        [HttpPost(Name = "GetMoves")]
        public IActionResult Get([FromBody] dynamic data)//(string cur_session)
        {
            CurSession curs = new CurSession();
            try
            {
                curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    output result = new output();
                    result.moveslist = _db.GetMovesList(curs.current_session!);
                    //if (result.friendlist.Count > 0)
                    //{
                    return Ok(JsonSerializer.Serialize(result.moveslist));
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
        }
    }
}
