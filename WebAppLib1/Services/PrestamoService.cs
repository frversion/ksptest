using System.Collections.Generic;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository prestamoRepository;
        private readonly ILibroRepository libroRepository;

        public void DevolverLibro(int prestamoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Prestamo> GetAllPrestamos()
        {
            throw new NotImplementedException();
        }

        public Prestamo GetPrestamoById(int id)
        {
            throw new NotImplementedException();
        }

        public void PrestarLibro(int userId, int libroId)
        {
            throw new NotImplementedException();
        }
    }
}
