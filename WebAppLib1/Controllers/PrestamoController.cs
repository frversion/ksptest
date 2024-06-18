using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Controllers
{
    /// <summary>
    /// Clase controller para prestamo.
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService prestamoService;

        /// <summary>
        /// Constructor para clase controller.
        /// </summary>
        /// <param name="prestamoService">Nuevo objeto prestamo.</param>
        public PrestamoController(IPrestamoService prestamoService)
        {
            this.prestamoService = prestamoService;
        }

        /// <summary>
        /// Operacion de API: Obtener todos los prestamos.
        /// </summary>
        /// <returns>Lista de todos los prestamos disponibles.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Prestamo>> GetPrestamos()
        {
            return Ok(prestamoService.GetAllPrestamos());
        }

        /// <summary>
        /// Operacion de API: Obtener un prestamo especifico.
        /// </summary>
        /// <param name="id">Id del prestamo a recuperar.</param>
        /// <returns>Objeto prestamo que coincida con el criterio.</returns>
        [HttpGet("{id}")]
        public ActionResult<Prestamo> GetPrestamo(int id)
        {
            var prestamo = prestamoService.GetPrestamoById(id);

            if (prestamo == null)
            {
                return NotFound();
            }

            return Ok(prestamo);
        }
        /// <summary>
        /// Operacion API: Prestamo de un libro.
        /// </summary>
        /// <param name="usuarioId">Id del usuario que solicita el prestamo.</param>
        /// <param name="libroId">Id del libro que sera solicitado en prestamo.</param>
        /// <returns>Status 200 OK. O algun mensaje de error en su caso.</returns>
        [HttpPost]
        public IActionResult PrestarLibro(PrestamoParams prestamoParams)
        {
            try
            {
                prestamoService.PrestarLibro(prestamoParams);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Operacion API: Devolucion de un libro.
        /// </summary>
        /// <param name="prestamoId">Id del prestamo a ser devuelto.</param>
        /// <returns>Status 200 OK. O algun mensaje de error en su caso.</returns>
        [HttpPost("devolver/{prestamoId}")]
        public IActionResult DevolverLibro(int prestamoId)
        {
            try
            {
                prestamoService.DevolverLibro(prestamoId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
