using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Services
{
    /// <summary>
    /// Clase servicio para usuario.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;

        /// <summary>
        /// Constructor para la coase.
        /// </summary>
        /// <param name="usuarioRepository">Parametro de constructor objeto usuario.</param>
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Operacion de agregar usuario.
        /// </summary>
        /// <param name="usuario">Objeto usuario a agregar.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse AddUsuario(Usuario usuario)
        {
            return usuarioRepository.Add(usuario);
        }

        /// <summary>
        /// Operacion de eliminar usuario.
        /// </summary>
        /// <param name="id">Identificador de usuario a eliminar.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse DeleteUsuario(int id)
        {
            return usuarioRepository.Delete(id);
        }

        /// <summary>
        /// Operacion de obtener todos los usuarios disponibles.
        /// </summary>
        /// <returns>Lista de objetos usuarios disponibles.</returns>
        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return usuarioRepository.GetAll();
        }

        /// <summary>
        /// Operacion de obtener el usuario especifico.
        /// </summary>
        /// <param name="id">Numero de identificador de usuario a devolver.</param>
        /// <returns>Objeto usuario que coincida con el criterio especificado.</returns>
        public Usuario? GetUsuarioById(int id)
        {
            return usuarioRepository.GetById(id);
        }

        /// <summary>
        /// Operacion de actualizar el usuario especificado.
        /// </summary>
        /// <param name="usuario">Objeto de usuario a ser modificado.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse UpdateUsuario(Usuario usuario)
        {
            return usuarioRepository.Update(usuario);
        }

        /// <summary>
        /// Metodo que valida que el email y password coincida con alguno registrado en la BD.
        /// </summary>
        /// <param name="email">Email asociado al usuario.</param>
        /// <param name="password">Password asociado al usuario.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse ValidateUsuario(string email, string password)
        {
            var user = usuarioRepository.GetAll().FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return new ApiResponse { IsSuccess = false, ResultMessage = "Error en la validacion del usuario.", ErrorMessage = "Credenciales invalidas." };
                //throw new ArgumentException($"El usuario con el email {email} no existe.");
            }

            using (var sha = SHA256.Create())
            {
                var hashedpwd = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
                if (Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(user.Password))) == hashedpwd)
                {
                    return new ApiResponse { IsSuccess = true, ResultMessage = "Usuario autenticado con exito." };
                }
                else
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "No fue posible autenticar al usuario." };
                }
            }
        }
    }
}
