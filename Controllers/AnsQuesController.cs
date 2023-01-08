using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace server_red.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnsQuesController : Controller
    {
        class input
        {
            public string? token { get; set; }
            public string? ans { get; set; }
        }
        class output
        {
            public string? newToken { get; set; }
        }

        private readonly IRedRepository _db;
        public AnsQuesController(IRedRepository db)
        {
            _db = db;
        }
        [HttpPost(Name = "GetPassToken")]
        public IActionResult Get([FromBody] dynamic data)//(string token, string ans)
        {
            try
            {
                input res = JsonSerializer.Deserialize<input>(data);
                if (res != null)
                {
                    output newToken = new output();
                    newToken.newToken = _db.AnsQues(res.token!, res.ans!);

                    if (newToken.newToken != null && newToken.newToken.Length > 0)
                    {
                        return Ok(newToken);
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

            /*string newToken = _db.AnsQues(token, ans);

            if (newToken != null && newToken.Length > 0)
            {
                return Ok(newToken);
            }
            else
            {
                return BadRequest();
            }*/
        }
    }
}
