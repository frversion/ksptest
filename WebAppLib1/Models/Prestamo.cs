namespace WebAppLib1.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LibroId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public Boolean YaDevuelto { get; set; }

        public Usuario? Usuario { get; set; }
        public Libro? Libro { get; set; }
    }
}
