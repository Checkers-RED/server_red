using Microsoft.AspNetCore.Mvc;
using server_red.Models;
using System.Text.Json;


namespace server_red.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : Controller
    {
        class output
        {
            public string? nick { get; set; }
            public string? pass { get; set; }
        }


        private readonly IRedRepository _db;
        public AuthorizationController(IRedRepository db /*IConfiguration c*/)
        {
            _db = db; // RedRepository(c);
        }

        [HttpPost(Name = "GetAuthorization")]
        //public IEnumerable<WeatherForecast> Get(string nick, string pass)
        public IActionResult Get ([FromBody] dynamic data)//(string nick, string pass)

        {
            try
            {
                output res = JsonSerializer.Deserialize<output>(data);
                if (res != null)
                {
                    CurSession curs = new CurSession();

                    curs.current_session = _db.SignIn(res.nick!, res.pass!);

                    if (curs.current_session != null && curs.current_session != "none" && curs.current_session.Length > 0)
                    {
                        return Ok(JsonSerializer.Serialize(curs));
                        //return Ok(curs);
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

            /////string cur_session = _db.SignIn(res.nick, res.pass);

            /*Authorization a = new Authorization();
            string r = "rrr";
            string cur_s = "ccc";
            if (results[0] != null)
            {
                r = results[0];
                if (r != "0")
                {
                    cur_s = results[1];
                }
            }
            a.res = r;
            a.cur_s = cur_s;

            string cur_session = results[0];*/
            /////if (cur_session != null && cur_session != "none" && cur_session.Length > 0)
          /////  {
          /////      return Ok(cur_session);

          /////  }
         /////   else
         /////   {
          /////      return BadRequest();// BadRequest(400);
          ////  }
          //  return Ok(a);

            /*List<string> results = _db.SignIn(nick, pass);
            string res = results[0];
            string cur_session = "";
            if (res != "0")
            {
                cur_session = results[1];
            }
            WeatherForecast wf = new WeatherForecast();
            wf.res = res;
            wf.current_session = cur_session;

            if (res == "0")
            {
                return (IEnumerable<WeatherForecast>)NotFound();
            }
            else
            {
                return (IEnumerable<WeatherForecast>)Ok(wf);
            }*/

            /*  return Enumerable.Range(1, 5).Select(index => new WeatherForecast
              {
                  Date = DateTime.Now.AddDays(index),
                  TemperatureC = Random.Shared.Next(-20, 55),
                  Summary = Summaries[Random.Shared.Next(Summaries.Length)]
              })
              .ToArray();*/
        }

        /*
        // GET: AuthorizationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AuthorizationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthorizationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorizationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorizationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthorizationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorizationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthorizationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
