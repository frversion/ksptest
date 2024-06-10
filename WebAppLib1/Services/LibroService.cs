using System.Collections.Generic;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;
using WebAppLib1.Repository;

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
            if (string.IsNullOrEmpty(libro.Titulo) || libro.Copias < 0)
            {
                throw new ArgumentException("El título no puede estar vacío y las copias no pueden ser negativas.");
            }

            libroRepository.Add(libro);
        }

        public void DeleteLibro(int id)
        {
            libroRepository.Delete(id);
        }

        public IEnumerable<Libro> GetAllLibros()
        {
            return libroRepository.GetAll();
        }

        public Libro GetLibroById(int id)
        {
            return libroRepository.GetById(id);
        }

        public void UpdateLibro(Libro libro)
        {
            if (string.IsNullOrEmpty(libro.Titulo) || libro.Copias < 0)
            {
                throw new ArgumentException("El título no puede estar vacío y las copias no pueden ser negativas.");
            }

            libroRepository.Update(libro);
        }
    }
}
