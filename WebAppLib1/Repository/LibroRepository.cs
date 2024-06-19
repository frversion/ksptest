using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Repository
{
    /// <summary>
    /// Clase de repositorio para libro.
    /// </summary>
    public class LibroRepository : ILibroRepository
    {
        private readonly LibraryContext context;

        /// <summary>
        /// Constructor para la clase repositorio para libro.
        /// </summary>
        /// <param name="context">Parametro de contexto para el constructor.</param>
        public LibroRepository (LibraryContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Metodo agregar libro.
        /// </summary>
        /// <param name="libro">Objeto libro para ser agregado.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse Add(Libro libro)
        {
            try
            {
                if (string.IsNullOrEmpty(libro.Titulo) || string.IsNullOrEmpty(libro.Autor) || string.IsNullOrEmpty(libro.Categoria)
                    || string.IsNullOrEmpty(libro.AnioPublicacion) || libro.Copias < 0)
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = "Todos los campos son obligatorios, y copias debe ser mayor o igual a cero." };
                    //throw new ArgumentException("Todos los campos son obligatorios y copias debe ser mayor o igual a cero.");
                }

                if (context.Libros.Any(u => u.Titulo == libro.Titulo))
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = $"El libro con titulo {libro.Titulo} ya existe." };
                    //throw new ArgumentException($"El libro con titulo {libro.Titulo} ya existe.");
                }

                context.Libros.Add(libro);
                context.SaveChanges();
                return new ApiResponse { IsSuccess = true, ResultMessage = "El libro fue agregado exitosamente.", ResultObject = new { id = libro.Id, libro = libro } };
            }
            catch (Exception ex)
            {
                return new ApiResponse { IsSuccess = false, ResultMessage = "Excepcion", ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Metodo para eliminar libro.
        /// </summary>
        /// <param name="id">Id del objeto libro a eliminar.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse Delete(int id)
        {
            try
            {
                var libro = context.Libros.Find(id);

                if (libro == null)
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = $"El libro con Id {id} no existe." };
                    //throw new Exception($"El libro con Id {id} no existe.");
                }

                // Validar que exista el libro, y que no esté pendiente de devolver en algún prestamo
                if (!context.Prestamos.Any(p => p.LibroId == id && !p.YaDevuelto))
                {
                    context.Libros.Remove(libro);
                    context.SaveChanges();
                    return new ApiResponse { IsSuccess = true, ResultMessage = $"El libro con Id {id} fue eliminado exitosamente." };
                }
                else
                {
                    var userId = context.Prestamos.FirstOrDefault(p => p.LibroId == id && !p.YaDevuelto)?.UserId;
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = $"El libro tiene prestamos pendientes para el usuario: {context.Usuarios.FirstOrDefault(u => u.Id == userId)?.Username}" };
                    //throw new Exception($"El libro tiene prestamos pendientes para el usuario: { context.Usuarios.FirstOrDefault( u => u.Id == userId)?.Username }");
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse { IsSuccess = false, ResultMessage = "Excepcion", ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Devolver todos los libros disponibles.
        /// </summary>
        /// <returns>Lista de objetos tipo Libro.</returns>
        public IEnumerable<Libro> GetAll()
        {
            try
            {
                return context.Libros.ToList();
            }
            catch
            {
                return new List<Libro>();
            }
        }

        /// <summary>
        /// Devolver el libro especificado.
        /// </summary>
        /// <param name="id">Valor del id del libro a recuperar.</param>
        /// <returns>Objeto libro que conicida con el criterio especificado.</returns>
        public Libro? GetById(int id)
        {
            try
            {
                return context.Libros.Find(id);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Actualizar el libro especificado.
        /// </summary>
        /// <param name="libro">Objeto libro a ser modificado.</param>
        /// <returns>Tipo ApiResponse con informacion extendida sobre la operacion ejecutada.</returns>
        public ApiResponse Update(Libro libro)
        {
            try
            {
                if (string.IsNullOrEmpty(libro.Titulo) || string.IsNullOrEmpty(libro.Autor) || string.IsNullOrEmpty(libro.Categoria)
                    || string.IsNullOrEmpty(libro.AnioPublicacion) || libro.Copias < 0)
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = "Todos los campos son obligatorios, y copias debe ser mayor o igual a cero" };
                    //throw new ArgumentException("Todos los campos son obligatorios. Copias debe ser mayor o igual a cero.");
                }

                if (!context.Libros.Any(l => l.Id == libro.Id))
                {
                    return new ApiResponse { IsSuccess = false, ResultMessage = "Validacion fallida", ErrorMessage = $"No existe un libro con el Id {libro.Id}" };
                }

                context.Libros.Update(libro);
                context.SaveChanges();
                return new ApiResponse { IsSuccess = true, ResultMessage = "El libro fue actualizado exitosamente.", ResultObject = libro };
            }
            catch (Exception ex)
            {
                return new ApiResponse { IsSuccess = false, ResultMessage = "Excepcion", ErrorMessage = ex.Message };
            }
        }
    }
}
