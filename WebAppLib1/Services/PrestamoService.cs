using System.Collections.Generic;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;
using WebAppLib1.Repository;

namespace WebAppLib1.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository prestamoRepository;
        private readonly ILibroRepository libroRepository;

        public PrestamoService(IPrestamoRepository prestamoRepository, ILibroRepository libroRepository)
        {
            this.prestamoRepository = prestamoRepository;
            this.libroRepository = libroRepository;
        }

        public void DevolverLibro(int prestamoId)
        {
            var prestamo = prestamoRepository.GetById(prestamoId);

            if (prestamo == null || prestamo.YaDevuelto)
            {
                throw new InvalidOperationException("El préstamo no existe o ya ha sido devuelto.");
            }

            var libro = libroRepository.GetById(prestamo.LibroId);
            if (libro != null)
            {
                libro.Copias++;
                libroRepository.Update(libro);
            }

            prestamo.YaDevuelto = true;
            prestamo.FechaDevolucion = DateTime.Now;
            prestamoRepository.Update(prestamo);
        }

        public IEnumerable<Prestamo> GetAllPrestamos()
        {
            return prestamoRepository.GetAll();
        }

        public Prestamo GetPrestamoById(int id)
        {
            return prestamoRepository.GetById(id);
        }

        public void PrestarLibro(int userId, int libroId)
        {
            var libro = libroRepository.GetById(libroId);

            if (libro == null || libro.Copias == 0)
            {
                throw new InvalidOperationException("El libro no existe o no hay copias disponibles.");
            }

            var prestamo = new Prestamo
            {
                UserId = userId,
                LibroId = libroId,
                FechaPrestamo = DateTime.Now,
                YaDevuelto = false
            };

            libro.Copias--;
            libroRepository.Update(libro);
            prestamoRepository.Add(prestamo);
        }
    }
}
