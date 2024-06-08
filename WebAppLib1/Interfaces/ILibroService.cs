using System.Collections.Generic;
using WebAppLib1.Models;

namespace WebAppLib1.Interfaces
{
    public interface ILibroService
    {
        IEnumerable<Libro> GetAllLibros();
        Libro GetLibroById(int id);
        void AddLibro(Libro libro);
        void UpdateLibro(Libro libro);
        void DeleteLibro(int id);

    }
}
