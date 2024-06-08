using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
        {
            this.prestamoService = prestamoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Prestamo>> GetPrestamos()
        {
            return Ok(prestamoService.GetAllPrestamos());
        }

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

        [HttpPost]
        public IActionResult PrestarLibro(int usuarioId, int libroId)
        {
            try
            {
                prestamoService.PrestarLibro(usuarioId, libroId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
