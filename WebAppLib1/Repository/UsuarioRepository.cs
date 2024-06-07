using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LibraryContext context;

        public UsuarioRepository(LibraryContext context)
        {
            this.context = context;
        }

        public void Add(Usuario usuario)
        {
            // TODO: Agregar validación para evitar usuario repetido
            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Usuario usuario;
            usuario = context.Usuarios.Find(id);

            // Validar que exista el usuario, y que no tenga libros sin devolver
            if (usuario != null && context.Prestamos.Any(p => p.UserId == id && !p.YaDevuelto) )
            {
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
            }
        }

        public IEnumerable<Usuario> GetAll()
        {
            return context.Usuarios.ToList();
        }

        public Usuario GetById(int id)
        {
            return context.Usuarios.Find(id);
        }

        public void Update(Usuario usuario)
        {
            context.Usuarios.Update(usuario);
            context.SaveChanges();
        }
    }
}
