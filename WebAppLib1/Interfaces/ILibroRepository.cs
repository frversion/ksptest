namespace WebAppLib1.Interfaces
{
    using System.Collections.Generic;
    using WebAppLib1.Models;

    public interface ILibroRepository
    {
        IEnumerable<Libro> GetAll();

        Libro GetById(int id);

        void Add(Libro libro);
        void Update(Libro libro);
        void Delete(int id);
    }
}
