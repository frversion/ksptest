using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Controllers
{
    /// <summary>
    /// Clase controller para Libro.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService libroService;

        /// <summary>
        /// Constructor para clase controller.
        /// </summary>
        /// <param name="libroService">Nuevo objeto libro.</param>
        public LibroController(ILibroService libroService)
        {
            this.libroService = libroService;
        }

        /// <summary>
        /// Operacion de API: Obtener todos los libros.
        /// </summary>
        /// <returns>Lista de todos los objetos libros disponibles.</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> GetLibros()
        {
            var libros = libroService.GetAllLibros();

            if (!libros.Any())
            {
                return Ok(new ApiResponse { IsSuccess = false, ResultMessage = "La consulta no devolvio resultados." });
            }

            return Ok(libros);
        }

        /// <summary>
        /// Operacion de API: Obtener un libro especifico.
        /// </summary>
        /// <param name="id">Id del libro a recuperar.</param>
        /// <returns>Objeto libro que coincida con el criterio.</returns>
        [HttpGet("{id}")]
        public ActionResult<Libro> GetLibro(int id)
        {
            var libro = libroService.GetLibroById(id);

            if (libro == null)
            {
                return Ok(new ApiResponse { IsSuccess = false, ResultMessage = "La consulta no devolvio resultados." });
                //return NotFound();
            }

            return Ok(libro);
        }

        /// <summary>
        /// Operacion de API: Crear un nuevo libro.
        /// </summary>
        /// <param name="libro">Objeto del nuevo libro a ser creado.</param>
        /// <returns>Representacion del libro recien creado.</returns>
        [Authorize]
        [HttpPost]
        public ActionResult<Libro> CreateLibro(Libro libro)
        {
            return Ok(libroService.AddLibro(libro));
            //return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, libro);
        }

        /// <summary>
        /// Operacion de API: Actualizar un libro.
        /// </summary>
        /// <param name="id">Id del libro a ser actualizado.</param>
        /// <param name="libro">Objeto completo libro a ser actualizado.</param>
        /// <returns>Status 200 OK, o error si el libro a actualizar no corresponde al Id, o algun otro mensaje de error.</returns>
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateLibro(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return Ok(new ApiResponse { IsSuccess = false, ResultMessage = "El Id del libro recibido en el parametro no corresponde al Id del libro a actualizar." });
                //return BadRequest();
            }

            return Ok(libroService.UpdateLibro(libro));
            //return Ok(libro);
        }

        /// <summary>
        /// Operacion de API: Eliminar un libro.
        /// </summary>
        /// <param name="id">Id del libro a ser eliminado.</param>
        /// <returns>Status 200 OK, o error si el libro a eliminar tiene prestamos sin devolver, o algun otro mensaje de error.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteLibro(int id)
        {
            return Ok(libroService.DeleteLibro(id));
            //return Ok(id);
        }
    }
}
