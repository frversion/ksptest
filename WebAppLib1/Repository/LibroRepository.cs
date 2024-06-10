using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Repository
{
    public class LibroRepository : ILibroRepository
    {
        private readonly LibraryContext context;

        public LibroRepository (LibraryContext context)
        {
            this.context = context;
        }

        public void Add(Libro libro)
        {
            // TODO: Agregar validación para evitar agregar un titulo de libro repetido
            context.Libros.Add(libro);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var libro = context.Libros.Find(id);

            // Validar que exista el libro, y que no esté pendiente de devolver en algún prestamo
            if (libro != null && !context.Prestamos.Any(p => p.LibroId == id && !p.YaDevuelto)) 
            {
                context.Libros.Remove(libro);
                context.SaveChanges();
            }
        }

        public IEnumerable<Libro> GetAll()
        {
            return context.Libros.ToList();
        }

        public Libro GetById(int id)
        {
            return context.Libros.Find(id);
        }

        public void Update(Libro libro)
        {
            context.Libros.Update(libro);
            context.SaveChanges();
        }
    }
}
