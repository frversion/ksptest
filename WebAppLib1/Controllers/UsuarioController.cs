using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;
using WebAppLib1.Services;

namespace WebAppLib1.Controllers
{
    /// <summary>
    /// Clase controller para usuario.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        /// <summary>
        /// Constructor para clase controller.
        /// </summary>
        /// <param name="usuarioService">Nuevo objeto usuario.</param>
        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        /// <summary>
        /// Operacion de API: Obtener todos los usuarios.
        /// </summary>
        /// <returns>Lista de todos los objetos usuarios disponibles.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = usuarioService.GetAllUsuarios();

            if (!usuarios.Any())
            {
                return Ok(new ApiResponse { IsSuccess = true, ResultMessage = "La consulta no devolvio resultados." });
            }
            return Ok(usuarios);
        }

        /// <summary>
        /// Operacion de API: Obtener un libro especifico.
        /// </summary>
        /// <param name="id">Id del usuario a recuperar.</param>
        /// <returns>Objeto usuario que coincida con el criterio.</returns>
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = usuarioService.GetUsuarioById(id);

            if (usuario == null)
            {
                return Ok(new ApiResponse { IsSuccess = false, ResultMessage = "La consulta no devolvio resultados." });
                //return NotFound();
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Operacion de API: Crear un usuario.
        /// </summary>
        /// <param name="usuario">Objeto del nuevo usuario a ser creado.</param>
        /// <returns>Representacion del usuario recien creado.</returns>
        [HttpPost]
        public ActionResult<Usuario> CreateUsuario(Usuario usuario)
        {
            return Ok(usuarioService.AddUsuario(usuario));
            //return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        /// <summary>
        /// Operacion de API: Actualizar un usuario.
        /// </summary>
        /// <param name="id">Id del usuario a ser actualizado.</param>
        /// <param name="usuario">Objeto completo usuario a ser actualizado.</param>
        /// <returns>Status 200 OK, o error si el libro a actualizar no corresponde al Id, o algun otro mensaje de error.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return Ok(new ApiResponse { IsSuccess = false, ResultMessage = "El Id del usuario recibido en el parametro no corresponde al Id del usuario a actualizar." });
                //return BadRequest();
            }

            return Ok(usuarioService.UpdateUsuario(usuario));
            //return NoContent();
        }

        /// <summary>
        /// Operacion de API: Eliminar un usuario.
        /// </summary>
        /// <param name="id">Id del usuario a ser eliminado.</param>
        /// <returns>Status OK, o error si el usuario a eliminar tiene prestamos sin devolver, o algun otro mensaje de error.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            return Ok(usuarioService.DeleteUsuario(id));
            //return NoContent();
        }

        /// <summary>
        /// Operacion de API: Autenticar usuario.
        /// </summary>
        /// <param name="model">Objeto de usuario para gestion de la autenticacion.</param>
        /// <returns>200 OK acompanando el token de autenticacion. De lo contrario codigo 401 Unauthorized.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (usuarioService.ValidateUsuario(model.Email, model.Password))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, model.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CustomClaveSecretaSuperSegura123!ForAuthentication%KSP"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "kspapp",
                    audience: "kspapp",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }
    }

    /// <summary>
    /// Clase que encapsula los campos necesarios para la autenticacion.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Campo email.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Campo password.
        /// </summary>
        public required string Password { get; set; }
    }
}
