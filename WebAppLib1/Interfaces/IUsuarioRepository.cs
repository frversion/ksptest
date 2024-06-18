namespace WebAppLib1.Interfaces
{

    using System.Collections.Generic;
    using WebAppLib1.Models;

    public interface IUsuarioRepository
    {
        /// <summary>
        /// Devolver todos los usuarios disponibles.
        /// </summary>
        /// <returns>Lista de objetos tipo Usuario.</returns>
        IEnumerable<Usuario> GetAll();

        /// <summary>
        /// Devolver el usuario especificado.
        /// </summary>
        /// <param name="id">Valor del id del usuario a recuperar.</param>
        /// <returns>Objeto usuario que conicida con el criterio especificado.</returns>
        Usuario? GetById(int id);

        /// <summary>
        /// Metodo agregar usuario.
        /// </summary>
        /// <param name="usuario">Objeto usuario para ser agregado.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse Add (Usuario usuario);

        /// <summary>
        /// Actualizar el usuario especificado.
        /// </summary>
        /// <param name="usuario">Objeto usuario a ser modificado.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse Update (Usuario usuario);

        /// <summary>
        /// Metodo para eliminar usuario.
        /// </summary>
        /// <param name="id">Id del objeto usuario a eliminar.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse Delete(int id);
    }
}
