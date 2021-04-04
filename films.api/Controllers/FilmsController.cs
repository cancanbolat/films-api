using films.api.Models.Mongo;
using films.api.Services.Mongo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly FilmService _service;
        public FilmsController(FilmService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Films>> Get() => _service.GetAll();

        [HttpGet("{id:length(24)}", Name = "GetFilm")]
        public ActionResult<Films> Get(string id)
        {
            var film = _service.GetById(id);

            if (film == null)
                return NotFound();

            return film;
        }

        [HttpPost]
        public IActionResult Create(Films model)
        {
            _service.Add(model);
            return CreatedAtRoute("GetFilm", new { id = model.Id }, model);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Films model)
        {
            var film = _service.GetById(id);

            if (film == null)
                return NotFound();

            _service.Update(id, model);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var film = _service.GetById(id);

            if (film == null)
                return NotFound();

            _service.Delete(film);

            return NoContent();
        }
    }
}
