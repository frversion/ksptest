using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService libroService;

        public LibroController(ILibroService libroService)
        {
            libroService = libroService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> GetLibros()
        {
            return Ok(libroService.GetAllLibros());
        }

        [HttpGet("{id}")]
        public ActionResult<Libro> GetLibro(int id)
        {
            var libro = libroService.GetLibroById(id);

            if (libro == null)
            {
                return NotFound();
            }

            return Ok(libro);
        }

        [HttpPost]
        public ActionResult<Libro> CreateLibro(Libro libro)
        {
            libroService.AddLibro(libro);

            return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, libro);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLibro(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

            libroService.UpdateLibro(libro);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLibro(int id)
        {
            libroService.DeleteLibro(id);
            return NoContent();
        }
    }
}
