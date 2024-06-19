namespace WebAppLib1.Models
{
    /// <summary>
    /// Clase prestamo, especifica para solicitar parametros en el controller de Prestamos.
    /// </summary>
    public class PrestamoParams
    {
        /// <summary>
        /// Llave foranea a tabla de usuarios.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Llave foranea a tabla de libros.
        /// </summary>
        public int LibroId { get; set; }
    }
}
