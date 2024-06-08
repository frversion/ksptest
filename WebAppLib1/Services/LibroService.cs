using System.Collections.Generic;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository libroRepository;

        public LibroService(ILibroRepository libroRepository)
        {
            this.libroRepository = libroRepository;
        }

        public void AddLibro(Libro libro)
        {
            throw new NotImplementedException();
        }

        public void DeleteLibro(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Libro> GetAllLibros()
        {
            throw new NotImplementedException();
        }

        public Libro GetLibroById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateLibro(Libro libro)
        {
            throw new NotImplementedException();
        }
    }
}
