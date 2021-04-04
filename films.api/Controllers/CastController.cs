using films.api.Models;
using films.api.Services;
using films.api.Services.Mongo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace films.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly CastService _service;
        public CastController(CastService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Cast>> Get() => _service.GetAll();

        [HttpGet("{id:length(24)}", Name = "GetCast")]
        public ActionResult<Cast> Get(string id)
        {
            var cast = _service.GetById(id);

            if (cast == null)
                return NotFound();

            return cast;
        }

        [HttpPost]
        public IActionResult Create(Cast model)
        {
            _service.Add(model);
            return CreatedAtRoute("GetCast", new { id = model.Id }, model);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Cast model)
        {
            var cast = _service.GetById(id);

            if (cast == null)
                return NotFound();

            _service.Update(id, model);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var cast = _service.GetById(id);

            if (cast == null)
                return NotFound();

            _service.Delete(cast);

            return NoContent();
        }
    }
}
