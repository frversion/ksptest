using System.Collections.Generic;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;
using WebAppLib1.Repository;

namespace WebAppLib1.Services
{
    /// <summary>
    /// Clase servicio para Libro.
    /// </summary>
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository libroRepository;
        
        /// <summary>
        /// Constructor para la clase.
        /// </summary>
        /// <param name="libroRepository">Parametro de constructor objeto libro.</param>
        public LibroService(ILibroRepository libroRepository)
        {
            this.libroRepository = libroRepository;
        }

        /// <summary>
        /// Operacion de agregar libro.
        /// </summary>
        /// <param name="libro">Objeto libro a agregar.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse AddLibro(Libro libro)
        {
            return this.libroRepository.Add(libro);
        }

        /// <summary>
        /// Operacion de eliminar libro.
        /// </summary>
        /// <param name="id">Identificador de libro a eliminar.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse DeleteLibro(int id)
        {
            return this.libroRepository.Delete(id);
        }

        /// <summary>
        /// Operacion de obtener todos los libros disponibles.
        /// </summary>
        /// <returns>Lista de objetos libro disponibles.</returns>
        public IEnumerable<Libro> GetAllLibros()
        {
            return libroRepository.GetAll();
        }

        /// <summary>
        /// Operacion de obtener el libro especifico.
        /// </summary>
        /// <param name="id">Numero de identificador de libro a devolver.</param>
        /// <returns>Objeto libro que coincida con el criterio especificado.</returns>
        public Libro? GetLibroById(int id)
        {
            return libroRepository.GetById(id);
        }

        /// <summary>
        /// Operacion de actualizar el libro especificado.
        /// </summary>
        /// <param name="libro">Objeto de libro a ser modificado.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse UpdateLibro(Libro libro)
        {
            return this.libroRepository.Update(libro);
        }
    }
}
