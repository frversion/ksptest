using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Repository
{
    /// <summary>
    /// Clase repositorio para Prestamo.
    /// </summary>
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly LibraryContext context;

        /// <summary>
        /// Constructor para la clase repositorio para prestamo.
        /// </summary>
        /// <param name="context">Parametro de contexto para constructor.</param>
        public PrestamoRepository(LibraryContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Metodo agregar prestamo.
        /// </summary>
        /// <param name="prestamo">Objeto prestamo para ser agregado.</param>
        public void Add(Prestamo prestamo)
        {
            context.Prestamos.Add(prestamo);
            context.SaveChanges();
        }

        /// <summary>
        /// Devolver todos los prestamos disponibles.
        /// </summary>
        /// <returns>Lista de objetos tipo Prestamo.</returns>
        public IEnumerable<Prestamo> GetAll()
        {
            return context.Prestamos.Include(p => p.Usuario).Include(p => p.Libro).ToList();
        }

        /// <summary>
        /// Devolver el prestamo especificado.
        /// </summary>
        /// <param name="id">Valor del id del prestamo a recuperar.</param>
        /// <returns>Objeto prestamo que conicida con el criterio especificado.</returns>
        public Prestamo GetById(int id)
        {
            return context.Prestamos.Include(p => p.Usuario).Include(p => p.Libro).FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Actualizar el prestamo especificado.
        /// </summary>
        /// <param name="prestamo">Objeto prestamo a ser modificado.</param>
        public void Update(Prestamo prestamo)
        {
            context.Prestamos.Update(prestamo);
            context.SaveChanges();
        }
    }
}
