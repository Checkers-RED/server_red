using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;


namespace server_red.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetActiveColorController : Controller
    {
        class output
        {
            public string? active_color { get; set; }
        }

        private readonly IRedRepository _db;
        public GetActiveColorController(IRedRepository db /*IConfiguration c*/)
        {
            _db = db; // RedRepository(c);
        }

        [HttpPost(Name = "GetActiveColor")]
        //public IEnumerable<WeatherForecast> Get(string nick, string pass)
        public IActionResult Get([FromBody] dynamic data)//(string nick, string pass)

        {
            try
            {
                CurSession curs = JsonSerializer.Deserialize<CurSession>(data);
                if (curs != null)
                {
                    output res = new output();

                    res.active_color = _db.GetActiveColor(curs.current_session);

                    if (res.active_color != null && res.active_color != "none" && res.active_color.Length > 0)
                    {
                        return Ok(JsonSerializer.Serialize(res));
                    }
                    else
                    {
                        return BadRequest();// BadRequest(400);
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
