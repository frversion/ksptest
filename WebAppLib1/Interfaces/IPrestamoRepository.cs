namespace WebAppLib1.Interfaces
{
    using System.Collections.Generic;
    using WebAppLib1.Models;

    /// <summary>
    /// Interface repositorio para Prestamo.
    /// </summary>
    public interface IPrestamoRepository
    {

        /// <summary>
        /// Devolver todos los prestamos disponibles.
        /// </summary>
        /// <returns>Lista de objetos tipo Prestamo.</returns>
        IEnumerable<Prestamo> GetAll();

        /// <summary>
        /// Devolver el prestamo especificado.
        /// </summary>
        /// <param name="id">Valor del id del prestamo a recuperar.</param>
        /// <returns>Objeto prestamo que conicida con el criterio especificado.</returns>
        Prestamo? GetById(int id);

        /// <summary>
        /// Metodo agregar prestamo.
        /// </summary>
        /// <param name="prestamo">Objeto prestamo para ser agregado.</param>
        void Add(Prestamo prestamo);

        /// <summary>
        /// Actualizar el prestamo especificado.
        /// </summary>
        /// <param name="prestamo">Objeto prestamo a ser modificado.</param>
        void Update(Prestamo prestamo);
    }
}
