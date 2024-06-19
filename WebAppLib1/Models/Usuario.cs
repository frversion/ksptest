namespace WebAppLib1.Models
{
    /// <summary>
    /// Clase usuario.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador unico de usuario.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Username del usuario.
        /// </summary>
        public required string Username { get; set; }
        
        /// <summary>
        /// Password del usuario.
        /// </summary>
        public required string Password { get; set; }
        
        /// <summary>
        /// Email del usuario.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Referencia opcional a los prestamos asociados al usuario.
        /// </summary>
        public ICollection<Prestamo>? Prestamos { get; set; }
    }
}
