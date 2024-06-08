using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Repository
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly LibraryContext context;

        public PrestamoRepository(LibraryContext context)
        {
            this.context = context;
        }

        public void Add(Prestamo prestamo)
        {
            context.Prestamos.Add(prestamo);
            context.SaveChanges();
        }

        public IEnumerable<Prestamo> GetAll()
        {
            return context.Prestamos.Include(p => p.Usuario).Include(p => p.Libro).ToList();
        }

        public Prestamo GetById(int id)
        {
            return context.Prestamos.Include(p => p.Usuario).Include(p => p.Libro).FirstOrDefault(p => p.Id == id);
        }

        public void Update(Prestamo prestamo)
        {
            context.Prestamos.Update(prestamo);
            context.SaveChanges();
        }
    }
}
