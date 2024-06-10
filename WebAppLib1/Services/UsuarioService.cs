using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public void AddUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Username) || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Password))
            {
                throw new ArgumentException("Todos los campos son obligatorios.");
            }

            usuarioRepository.Add(usuario);
        }

        public void DeleteUsuario(int id)
        {
            usuarioRepository.Delete(id);
        }

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return usuarioRepository.GetAll();
        }

        public Usuario GetUsuarioById(int id)
        {
            return usuarioRepository.GetById(id);
        }

        public void UpdateUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Username) || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Password))
            {
                throw new ArgumentException("Todos los campos son obligatorios.");
            }

            usuarioRepository.Update(usuario);
        }

        public bool ValidateUsuario(string email, string password)
        {
            var user = usuarioRepository.GetAll().FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            using (var sha = SHA256.Create())
            {
                var hashedpwd = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(user.Password))) == hashedpwd;
            }
        }
    }
}
