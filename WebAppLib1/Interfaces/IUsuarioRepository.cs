namespace WebAppLib1.Interfaces
{

    using System.Collections.Generic;
    using WebAppLib1.Models;

    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetAll();

        Usuario GetById(int id);
        void Add (Usuario usuario);
        void Update (Usuario usuario);
        void Delete(int id);
    }
}
