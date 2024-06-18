namespace WebAppLib1.Models
{
    /// <summary>
    /// Clase Prestamo.
    /// </summary>
    public class Prestamo
    {
        /// <summary>
        /// Identificador unico de prestamo.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Llave foranea a tabla de usuarios.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Llave foranea a tabla de libros.
        /// </summary>
        public int LibroId { get; set; }

        /// <summary>
        /// Fecha en que se presto el libro.
        /// </summary>
        public DateTime FechaPrestamo { get; set; }
        
        /// <summary>
        /// Fecha en que se devolvió el libro.
        /// </summary>
        public DateTime? FechaDevolucion { get; set; }
        
        /// <summary>
        /// Bandera que indica si el libro ya se devolvió.
        /// </summary>
        public Boolean YaDevuelto { get; set; }

        /// <summary>
        /// Referencia opcional al usuario asociado al prestamo.
        /// </summary>
        public Usuario? Usuario { get; set; }
        
        /// <summary>
        /// Referencia opcional al libro asociado al prestamo.
        /// </summary>
        public Libro? Libro { get; set; }
    }
}
