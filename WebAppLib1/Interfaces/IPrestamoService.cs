using System.Collections.Generic;
using WebAppLib1.Models;

namespace WebAppLib1.Interfaces
{
    /// <summary>
    /// Interface servicio para prestamo.
    /// </summary>
    public interface IPrestamoService
    {
        /// <summary>
        /// Operacion de obtener todos los libros disponibles.
        /// </summary>
        /// <returns>Lista de objetos prestamo disponibles.</returns>
        IEnumerable<Prestamo> GetAllPrestamos();

        /// <summary>
        /// Operacion de obtener el prestamo especifico.
        /// </summary>
        /// <param name="id">Numero de identificador de prestamo a devolver.</param>
        /// <returns>Objeto prestamo que coincida con el criterio especificado.</returns>
        Prestamo GetPrestamoById(int id);

        /// <summary>
        /// Operacion prestamo de libro.
        /// </summary>
        /// <param name="prestamoParams">Entidad que contiene el userId y libroId, que corresponde al usuario que solicita el prestamo y el libro solicitado.</param>
        /// <exception cref="InvalidOperationException">En caso de excepcion, se devuelve el texto del error al caller.</exception>
        void PrestarLibro(PrestamoParams prestamoParams);

        /// <summary>
        /// Operacion de devolver libro en prestamo.
        /// </summary>
        /// <param name="prestamoId"></param>
        /// <exception cref="InvalidOperationException">En caso de excepcion, se devuelve el texto del error al caller.</exception>
        void DevolverLibro(int prestamoId);
    }
}
