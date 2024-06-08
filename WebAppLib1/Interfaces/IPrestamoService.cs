using System.Collections.Generic;
using WebAppLib1.Models;

namespace WebAppLib1.Interfaces
{
    public interface IPrestamoService
    {
        IEnumerable<Prestamo> GetAllPrestamos();
        Prestamo GetPrestamoById(int id);
        void PrestarLibro(int userId, int libroId);
        void DevolverLibro(int prestamoId);
    }
}
