namespace WebAppLib1.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public string AnioPublicacion { get; set; }
        public int Copias { get; set; }

        public ICollection<Prestamo> Prestamos { get; set; }
    }
}
