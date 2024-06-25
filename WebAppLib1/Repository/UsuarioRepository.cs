using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Repository
{
    /// <summary>
    /// Clase repositorio para Usuario.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LibraryContext context;

        /// <summary>
        /// Constructor para la clase repositorio para usuario.
        /// </summary>
        /// <param name="context">Parametro de contexto para constructor.</param>
        public UsuarioRepository(LibraryContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Metodo agregar usuario.
        /// </summary>
        /// <param name="usuario">Objeto usuario para ser agregado.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse Add(Usuario usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.Username) || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Password))
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = "Todos los campos son obligatorios." };
                    //throw new ArgumentException("Todos los campos son obligatorios.");
                }

                if (context.Usuarios.Any(u => u.Username == usuario.Username || u.Email == usuario.Email))
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = $"El usuario con nombre {usuario.Username} o email {usuario.Email} ya existe." };
                    //throw new ArgumentException($"El usuario con nombre {usuario.Username} o email {usuario.Email} ya existe.");
                }

                context.Usuarios.Add(usuario);
                context.SaveChanges();
                return new ApiResponse { IsSuccess = true, ResultMessage = "El usuario fue agregado exitosamente.", ResultObject = new { id = usuario.Id, libro = usuario } };
            }
            catch (Exception ex)
            {
                return new ApiResponse { IsSuccess = false, ResultMessage = "Excepcion", ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Metodo para eliminar usuario.
        /// </summary>
        /// <param name="id">Id del objeto usuario a eliminar.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse Delete(int id)
        {
            try
            {
                var usuario = context.Usuarios.Find(id);

                // Validar que exista el usuario
                if (usuario == null)
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = $"El usuario con Id {id} no se puede eliminar porque no existe." };
                    //throw new ArgumentException($"El usuario con Id {id} no se puede eliminar porque no existe.");
                }

                // Validar que el usuario no tenga libros sin devolver
                if (context.Prestamos.Any(p => p.UserId == id && !p.YaDevuelto))
                {
                    var libroId = context.Prestamos.FirstOrDefault(p => p.UserId == id && !p.YaDevuelto)?.LibroId;
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = $"El usuario tiene prestamos pendientes para el libro: {context.Libros.FirstOrDefault(u => u.Id == libroId)?.Titulo}" };

                    //throw new ArgumentException($"El usuario con Id {id} tiene prestamos de libros sin devolver. No se puede eliminar.");
                }

                context.Usuarios.Remove(usuario);
                context.SaveChanges();
                return new ApiResponse { IsSuccess = true, ResultMessage = $"El usuario con Id {id} fue eliminado exitosamente." };
            }
            catch (Exception ex)
            {
                return new ApiResponse { IsSuccess = false, ResultMessage = "Excepcion", ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Devolver todos los usuarios disponibles.
        /// </summary>
        /// <returns>Lista de objetos tipo Usuario.</returns>
        public IEnumerable<Usuario> GetAll()
        {
            try
            {
                return context.Usuarios.ToList();
            }
            catch
            {
                return new List<Usuario>();
            }
        }

        /// <summary>
        /// Devolver el usuario especificado.
        /// </summary>
        /// <param name="id">Valor del id del usuario a recuperar.</param>
        /// <returns>Objeto usuario que conicida con el criterio especificado.</returns>
        public Usuario? GetById(int id)
        {
            try
            {
                return context.Usuarios.Find(id);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Actualizar el usuario especificado.
        /// </summary>
        /// <param name="libro">Objeto usuario a ser modificado.</param>
        public ApiResponse Update(Usuario usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.Username) || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Password))
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = "Todos los campos son obligatorios" };
                    //throw new ArgumentException("Todos los campos son obligatorios.");
                }

                if (!context.Usuarios.Any(l => l.Id == usuario.Id))
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = $"No existe un usuario con el Id {usuario.Id}" };
                }

                // Excluyendo al usuario actual, no debe existir otro con el mismo usuario o correo.
                if (context.Usuarios.Any(u => u.Id == usuario.Id && (u.Username == usuario.Username || u.Email == usuario.Email)))
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = $"El usuario con nombre {usuario.Username} o email {usuario.Email} ya existe." };
                    //throw new ArgumentException($"El usuario con nombre {usuario.Username} o email {usuario.Email} ya existe.");
                }

                context.Usuarios.Update(usuario);
                context.SaveChanges();
                return new ApiResponse { IsSuccess = true, ResultMessage = "El usuario fue actualizado exitosamente.", ResultObject = usuario };
            }
            catch (Exception ex)
            {
                return new ApiResponse { IsSuccess = false, ResultMessage = "Excepcion", ErrorMessage = ex.Message };
            }
        }
    }
}
