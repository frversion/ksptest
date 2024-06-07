namespace WebAppLib1.Interfaces
{
    using System.Collections.Generic;
    using WebAppLib1.Models;

    public interface IPrestamoRepository
    {
        IEnumerable<Prestamo> GetAll();

        Prestamo GetById(int id);
        void Add(Prestamo prestamo);
        void Update(Prestamo prestamo);
    }
}
