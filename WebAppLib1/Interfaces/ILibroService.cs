using System.Collections.Generic;
using WebAppLib1.Models;

namespace WebAppLib1.Interfaces
{
    /// <summary>
    /// interface servicio para Libro.
    /// </summary>
    public interface ILibroService
    {
        /// <summary>
        /// Operacion de obtener todos los libros disponibles.
        /// </summary>
        /// <returns>Lista de objetos libro disponibles.</returns>
        IEnumerable<Libro> GetAllLibros();

        /// <summary>
        /// Operacion de obtener el libro especifico.
        /// </summary>
        /// <param name="id">Numero de identificador de libro a devolver.</param>
        /// <returns>Objeto libro que coincida con el criterio especificado.</returns>
        Libro? GetLibroById(int id);

        /// <summary>
        /// Operacion de agregar libro.
        /// </summary>
        /// <param name="libro">Objeto libro a agregar.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse AddLibro(Libro libro);

        /// <summary>
        /// Operacion de actualizar el libro especificado.
        /// </summary>
        /// <param name="libro">Objeto de libro a ser modificado.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse UpdateLibro(Libro libro);

        /// <summary>
        /// Operacion de eliminar libro.
        /// </summary>
        /// <param name="id">Identificador de libro a eliminar.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse DeleteLibro(int id);

    }
}
