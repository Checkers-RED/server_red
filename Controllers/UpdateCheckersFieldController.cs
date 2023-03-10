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
            public string? current_session { get; set; }
            public Checker[]? white { get; set; }
            public Checker[]? black { get; set; }
            public int previous_horiz { get; set; }
            public int previous_vertic { get; set; }
            public int new_horiz { get; set; }
            public int new_vertic { get; set; }
        }

        private readonly IRedRepository _db;
        public UpdateCheckersFieldController(IRedRepository db)
        {
            _db = db;
        }
        [HttpPost(Name = "UpdateCheckersField")]
        public IActionResult Get([FromBody] dynamic data)
        {
            try
            {
                input inres = JsonSerializer.Deserialize<input>(data);
                if (inres != null)
                {
                   // Console.WriteLine("inres is not null");
                    for(int i = 0; i < inres.white!.Length; i++)
                    {
                       // Console.WriteLine($"White i={i}");
                        bool delete_old = false; // нужно ли удалять все шашки текущего матча (удаляются при занесении 1й шашки)
                        if (i == 0)
                        {
                            delete_old = true;
                        }
                        int res = _db.UpdateCheckersField(inres.current_session!, inres.white[i].color!, inres.white[i].vertic, inres.white[i].horiz, Convert.ToInt32(inres.white[i].isQueen), Convert.ToInt32(inres.white[i].isBeaten), delete_old);
                        //int res = _db.UpdateCheckersField(inres.current_session!, inres.white[i].color!, inres.white[i].horiz, inres.white[i].vertic, Convert.ToInt32(inres.white[i].isQueen), Convert.ToInt32(inres.white[i].isBeaten), delete_old);
                        // Console.WriteLine($"After White UpdateCheckersField {i}");
                        if (res == 0) // с какой-то из шашек проблема. тут потеряются все шашки, которые были до удаления (начала обновления), а новые до конца не проставятся -_-
                        {
                           // Console.WriteLine($"White UpdateCheckersField res == 0, i == {i}");
                            return BadRequest();
                        }
                    }
                    for (int i = 0; i < inres.black!.Length; i++)
                    {
                       // Console.WriteLine($"Black i={i}");
                        bool delete_old = false;
                        int res = _db.UpdateCheckersField(inres.current_session!, inres.black[i].color!, inres.black[i].vertic, inres.black[i].horiz, Convert.ToInt32(inres.black[i].isQueen), Convert.ToInt32(inres.black[i].isBeaten), delete_old);
                        //int res = _db.UpdateCheckersField(inres.current_session!, inres.black[i].color!, inres.black[i].horiz, inres.black[i].vertic, Convert.ToInt32(inres.black[i].isQueen), Convert.ToInt32(inres.black[i].isBeaten), delete_old);
                       // Console.WriteLine($"After Bkack UpdateCheckersField {i}");
                        if (res == 0) // с какой-то из шашек проблема. тут потеряются все шашки, которые были до удаления (начала обновления), а новые до конца не проставятся -_-
                        {
                           // Console.WriteLine($"Black UpdateCheckersField res == 0, i == {i}");
                            return BadRequest();
                        }
                    }

                    if (inres.previous_horiz > 0 && inres.previous_vertic > 0 && inres.new_horiz > 0 && inres.new_vertic > 0)
                    {
                       // Console.WriteLine("If about previous_horiz etc...on line 66....");
                        string prev = "";
                        switch (inres.previous_horiz)
                        {
                            case 1:
                                {
                                    prev = "A" + inres.previous_vertic.ToString();
                                    break;
                                }
                            case 2:
                                {
                                    prev = "B" + inres.previous_vertic.ToString();
                                    break;
                                }
                            case 3:
                                {
                                    prev = "C" + inres.previous_vertic.ToString();
                                    break;
                                }
                            case 4:
                                {
                                    prev = "D" + inres.previous_vertic.ToString();
                                    break;
                                }
                            case 5:
                                {
                                    prev = "E" + inres.previous_vertic.ToString();
                                    break;
                                }
                            case 6:
                                {
                                    prev = "F" + inres.previous_vertic.ToString();
                                    break;
                                }
                            case 7:
                                {
                                    prev = "G" + inres.previous_vertic.ToString();
                                    break;
                                }
                            case 8:
                                {
                                    prev = "H" + inres.previous_vertic.ToString();
                                    break;
                                }
                            default:
                                break;
                        }
                        string newh = "";

                        switch (inres.new_horiz)
                        {
                            case 1:
                                {
                                    newh = "A" + inres.new_vertic.ToString();
                                    break;
                                }
                            case 2:
                                {
                                    newh = "B" + inres.new_vertic.ToString();
                                    break;
                                }
                            case 3:
                                {
                                    newh = "C" + inres.new_vertic.ToString();
                                    break;
                                }
                            case 4:
                                {
                                    newh = "D" + inres.new_vertic.ToString();
                                    break;
                                }
                            case 5:
                                {
                                    newh = "E" + inres.new_vertic.ToString();
                                    break;
                                }
                            case 6:
                                {
                                    newh = "F" + inres.new_vertic.ToString();
                                    break;
                                }
                            case 7:
                                {
                                    newh = "G" + inres.new_vertic.ToString();
                                    break;
                                }
                            case 8:
                                {
                                    newh = "H" + inres.new_vertic.ToString();
                                    break;
                                }
                            default:
                                break;
                        }

                        string note = prev + "-" + newh;
                       // Console.WriteLine("Just before InsertMovesList..");
                        _db.InsertMovesList(inres.current_session!, note);
                       // Console.WriteLine("After InsertMovesList...");
                    }
                    return Ok();
                }
                else
                {
                   // Console.WriteLine("=( 1");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
               // Console.WriteLine("=( 2");
                return BadRequest();
            }
        }
    }
}
