using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using WebAppLib1.Controllers;
using WebAppLib1.Models;
using Microsoft.EntityFrameworkCore;
using WebAppLib1.DB;
using WebAppLib1.Services;
using WebAppLib1.Repository;
using Newtonsoft.Json;

namespace WebAppLib1.Test
{
    public class LibroControllerTests
    {
        private readonly LibroService libroService;
        private readonly LibroController controller;
        private readonly LibraryContext context;

        public LibroControllerTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            context = new LibraryContext(options);
            SeedDatabase();

            var libroRepository = new LibroRepository(context);
            libroService = new LibroService(libroRepository);

            controller = new LibroController(libroService);

        }

        private void SeedDatabase()
        {
            DBInitializer.Initialize(context);
        }

        [Fact]
        public void GetLibros_ReturnsOkResult_ConListaDeLibros()
        {
            //var result = controller.GetLibros();

            //var okResult = Assert.IsType<OkObjectResult>(result.Result);
            //Assert.IsType<List<Libro>>(okResult.Value);

            var result = controller.GetLibros();
            //var okResult = result as OkObjectResult;
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.IsType<List<Libro>>(okResult.Value);
        }

        [Fact]
        public void GetLibros_ReturnsFullLibroObjectResult_ConListaDeLibros()
        {
            // arrange
            var expectedLibros = context.Libros.ToList();

            // act
            var resultActionResult = controller.GetLibros();
            var result = Assert.IsType<OkObjectResult>(resultActionResult.Result).Value as List<Libro>;

            // assert

            Assert.Equal(expectedLibros, result);
        }

        [Fact]
        public void GetLibro_ReturnsOkyEmptyResult_CuandoLibroNoExista()
        {
            // arrange
            var libroIdNoExiste = -4;
            var expectedApiResponse = JsonConvert.SerializeObject( new ApiResponse { IsSuccess = false, ResultMessage = "La consulta no devolvio resultados." });

            // act
            var resultActionResult = controller.GetLibro(libroIdNoExiste).Result;
            var result = JsonConvert.SerializeObject(Assert.IsType<OkObjectResult>(resultActionResult).Value);

            // assert
            Assert.Equal(expectedApiResponse, result);

            //Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetLibro_ReturnsOkResult_ConLibro()
        {
            // arrange
            var expectedLibro = JsonConvert.SerializeObject(new Libro { Id = 3, Titulo = "Laberinto de la soledad", Autor = "Octavio Paz", Categoria = "Mexico", AnioPublicacion = "1950", Copias = 0 });
            var idLibro = 3;

            // act
            var resultActionResult = controller.GetLibro(idLibro).Result;
            var result = JsonConvert.SerializeObject(Assert.IsType<OkObjectResult>(resultActionResult).Value);
            //var returnedLibro = Assert.IsType<Libro>(okResult.Value);

            Assert.Equal(expectedLibro, result);
        }
    }
}
