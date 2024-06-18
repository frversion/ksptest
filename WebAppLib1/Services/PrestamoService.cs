using System.Collections.Generic;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;
using WebAppLib1.Repository;

namespace WebAppLib1.Services
{
    /// <summary>
    /// Clase servicio para prestamo.
    /// </summary>
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository prestamoRepository;
        private readonly ILibroRepository libroRepository;

        /// <summary>
        /// Constructor para la clase.
        /// </summary>
        /// <param name="prestamoRepository">Parametro de constructor objeto prestamo.</param>
        /// <param name="libroRepository">Parametro de constructor objeto libro.</param>
        public PrestamoService(IPrestamoRepository prestamoRepository, ILibroRepository libroRepository)
        {
            this.prestamoRepository = prestamoRepository;
            this.libroRepository = libroRepository;
        }

        /// <summary>
        /// Operacion de devolver libro en prestamo.
        /// </summary>
        /// <param name="prestamoId"></param>
        /// <exception cref="InvalidOperationException">En caso de excepcion, se devuelve el texto del error al caller.</exception>
        public void DevolverLibro(int prestamoId)
        {
            var prestamo = prestamoRepository.GetById(prestamoId);

            if (prestamo == null || prestamo.YaDevuelto)
            {
                throw new InvalidOperationException("El préstamo no existe o ya ha sido devuelto.");
            }

            var libro = libroRepository.GetById(prestamo.LibroId);
            if (libro != null)
            {
                libro.Copias++;
                libroRepository.Update(libro);
            }

            prestamo.YaDevuelto = true;
            prestamo.FechaDevolucion = DateTime.Now;
            prestamoRepository.Update(prestamo);
        }

        /// <summary>
        /// Operacion de obtener todos los libros disponibles.
        /// </summary>
        /// <returns>Lista de objetos prestamo disponibles.</returns>
        public IEnumerable<Prestamo> GetAllPrestamos()
        {
            return prestamoRepository.GetAll();
        }

        /// <summary>
        /// Operacion de obtener el prestamo especifico.
        /// </summary>
        /// <param name="id">Numero de identificador de prestamo a devolver.</param>
        /// <returns>Objeto prestamo que coincida con el criterio especificado.</returns>
        public Prestamo GetPrestamoById(int id)
        {
            return prestamoRepository.GetById(id);
        }

        /// <summary>
        /// Operacion prestamo de libro.
        /// </summary>
        /// <param name="userId">Id de usuario quien solicita el prestamo.</param>
        /// <param name="libroId">Id de libro que se esta solicitando.</param>
        /// <exception cref="InvalidOperationException">En caso de excepcion, se devuelve el texto del error al caller.</exception>
        public void PrestarLibro(PrestamoParams prestamoParams)
        {
            var libro = libroRepository.GetById(prestamoParams.LibroId);

            if (libro == null || libro.Copias == 0)
            {
                throw new InvalidOperationException("El libro no existe o no hay copias disponibles.");
            }

            var prestamo = new Prestamo
            {
                UserId = prestamoParams.UserId,
                LibroId = prestamoParams.LibroId,
                FechaPrestamo = DateTime.Now,
                YaDevuelto = false
            };

            libro.Copias--;
            libroRepository.Update(libro);
            prestamoRepository.Add(prestamo);
        }
    }
}
