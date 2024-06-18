namespace WebAppLib1.Interfaces
{
    using System.Collections.Generic;
    using WebAppLib1.Models;

    /// <summary>
    /// Interface de repositorio para Libro.
    /// </summary>
    public interface ILibroRepository
    {
        /// <summary>
        /// Devolver todos los libros disponibles.
        /// </summary>
        /// <returns>Lista de objetos tipo Libro.</returns>
        IEnumerable<Libro> GetAll();

        /// <summary>
        /// Devolver el libro especificado.
        /// </summary>
        /// <param name="id">Valor del id del libro a recuperar.</param>
        /// <returns>Objeto libro que conicida con el criterio especificado.</returns>
        Libro? GetById(int id);

        /// <summary>
        /// Metodo agregar libro.
        /// </summary>
        /// <param name="libro">Objeto libro para ser agregado.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse Add(Libro libro);

        /// <summary>
        /// Actualizar el libro especificado.
        /// </summary>
        /// <param name="libro">Objeto libro a ser modificado.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse Update(Libro libro);

        /// <summary>
        /// Eliminar el libro especificado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        ApiResponse Delete(int id);
    }
}
