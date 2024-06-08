using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;
using WebAppLib1.Services;

namespace WebAppLib1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            return Ok(usuarioService.GetAllUsuarios());
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = usuarioService.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public ActionResult<Usuario> CreateUsuario(Usuario usuario)
        {
            usuarioService.AddUsuario(usuario);

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            usuarioService.UpdateUsuario(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            usuarioService.DeleteUsuario(id);
            return NoContent();
        }
    }
}
