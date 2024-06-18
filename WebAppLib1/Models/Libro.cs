namespace WebAppLib1.Models
{
    /// <summary>
    /// Clase Libro.
    /// </summary>
    public class Libro
    {
        /// <summary>
        /// Identificador unico del libro.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Titulo del libro.
        /// </summary>
        public required string Titulo { get; set; }

        /// <summary>
        /// Autor del libro.
        /// </summary>
        public required string Autor { get; set; }
        
        /// <summary>
        /// Categoria del libro.
        /// </summary>
        public required string Categoria { get; set; }
        
        /// <summary>
        /// Anio de publicacion del libro.
        /// </summary>
        public required string AnioPublicacion { get; set; }
        
        /// <summary>
        /// Copias disponibles del libro.
        /// </summary>
        public required int Copias { get; set; }

        /// <summary>
        /// Propiedad opcional sobre prestamos asociados al libro.
        /// </summary>
        public ICollection<Prestamo>? Prestamos { get; set; }
    }
}
