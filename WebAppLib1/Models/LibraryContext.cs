namespace WebAppLib1.Models
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Clase que define el contexto para el modelo de datos.
    /// </summary>
    public class LibraryContext : DbContext
    {
        /// <summary>
        /// Consstructor de clase.
        /// </summary>
        /// <param name="options">Opciones para el constructor.</param>
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        /// <summary>
        /// Referencia de datos a la entidad Usuarios.
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }
        
        /// <summary>
        /// Referencia a la entidad Libros.
        /// </summary>
        public DbSet<Libro> Libros { get; set; }
        
        /// <summary>
        /// Referencia a la entidad Prestamos.
        /// </summary>
        public DbSet<Prestamo> Prestamos { get; set; }

        /// <summary>
        /// Definicion de la relacion entre entidades.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Prestamos)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Libro>()
                .HasMany(l => l.Prestamos)
                .WithOne(p => p.Libro)
                .HasForeignKey(p => p.LibroId);
        }
    }
}
