using Microsoft.AspNetCore.Mvc;

namespace Seton_Backend.Controllers
{
    [ApiController]
    [Route("api/notes")]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Notes_cs> GetNote()
        {
            var note = new Notes_cs
            {
                test = "test",
                first = "first"
            };

            return Ok(note); // Отправляем объект в JSON-формате
        }
    }
}
