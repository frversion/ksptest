﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebAppLib1.Models;

namespace WebAppLib1.DB
{
    public static class DBInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            // Si ya hay datos, salir
            if (context.Usuarios.Any() || context.Libros.Any() || context.Prestamos.Any())
            {
                return; 
            }

            var usuarios = new Usuario[]
            {
            new Usuario { Username = "Fer R", Email = "fer@ksp.com", Password = "Password123!" },
            new Usuario { Username = "Leo G", Email = "leo@ksp.com", Password = "Password12345!" },
            new Usuario { Username = "Hector H", Email = "hector@ksp.com", Password = "Password123!" }
            };

            foreach (var u in usuarios)
            {
                context.Usuarios.Add(u);
            }

            var libros = new Libro[]
            {
            new Libro { Titulo = "100 años de soledad", Autor = "Gabriel Garcia Marquez", Categoria = "Colombia", AnioPublicacion = "1945", Copias = 2 },
            new Libro { Titulo = "Rayuela", Autor = "Julio Cortazar", Categoria = "Argentina", AnioPublicacion = "1964", Copias = 4 },
            new Libro { Titulo = "Laberinto de la soledad", Autor = "Octavio Paz", Categoria = "Mexico", AnioPublicacion = "1950", Copias = 0 },
            new Libro { Titulo = "Piedra de sol", Autor = "Octavio Paz", Categoria = "Mexico", AnioPublicacion = "1950", Copias = 0 }
            };

            foreach (var l in libros)
            {
                context.Libros.Add(l);
            }

            var prestamos = new Prestamo[]
            {
            new Prestamo { UserId = 1, LibroId = 1, FechaPrestamo = DateTime.Now },
            new Prestamo { UserId = 2, LibroId = 2, FechaPrestamo = DateTime.Now },
            new Prestamo { UserId = 3, LibroId = 2, FechaPrestamo = DateTime.Now }
            };

            foreach (var p in prestamos)
            {
                context.Prestamos.Add(p);
            }

            context.SaveChanges();
        }
    }
}