using System.Collections.Generic;
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
            usuarioRepository.Update(usuario);
        }
    }
}
