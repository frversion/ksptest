using System.Collections.Generic;
using WebAppLib1.Models;

namespace WebAppLib1.Interfaces
{
    /// <summary>
    /// Interface servicio para usuario.
    /// </summary>
    public interface IUsuarioService
    {
        /// <summary>
        /// Operacion de obtener todos los usuarios disponibles.
        /// </summary>
        /// <returns>Lista de objetos usuarios disponibles.</returns>
        IEnumerable<Usuario> GetAllUsuarios();

        /// <summary>
        /// Operacion de obtener el usuario especifico.
        /// </summary>
        /// <param name="id">Numero de identificador de usuario a devolver.</param>
        /// <returns>Objeto usuario que coincida con el criterio especificado.</returns>
        Usuario? GetUsuarioById(int id);

        /// <summary>
        /// Operacion de agregar usuario.
        /// </summary>
        /// <param name="usuario">Objeto usuario a agregar.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse AddUsuario(Usuario usuario);

        /// <summary>
        /// Operacion de actualizar el usuario especificado.
        /// </summary>
        /// <param name="usuario">Objeto de usuario a ser modificado.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse UpdateUsuario(Usuario usuario);

        /// <summary>
        /// Operacion de eliminar usuario.
        /// </summary>
        /// <param name="id">Identificador de usuario a eliminar.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse DeleteUsuario(int id);

        /// <summary>
        /// Metodo que valida que el email y password coincida con alguno registrado en la BD.
        /// </summary>
        /// <param name="email">Email asociado al usuario.</param>
        /// <param name="password">Password asociado al usuario.</param>
        /// <returns>El email coincide o no con el password especificado.</returns>
        bool ValidateUsuario(string email, string pwd);
    }
}
