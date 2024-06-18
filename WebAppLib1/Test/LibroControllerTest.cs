using Moq;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using WebAppLib1.Controllers;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Test
{
    public class LibroControllerTests
    {
        private readonly Mock<ILibroService> mockLibroService;
        private readonly LibroController controller;

        public LibroControllerTests()
        {
            mockLibroService = new Mock<ILibroService>();
            controller = new LibroController(mockLibroService.Object);
        }

        [Fact]
        public void GetLibros_ReturnsOkResult_ConListaDeLibros()
        {
            mockLibroService.Setup(service => service.GetAllLibros()).Returns(new List<Libro>());

            var result = controller.GetLibros();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<Libro>>(okResult.Value);
        }

        [Fact]
        public void GetLibro_ReturnsNotFoundResult_CuandoLibroNoExista()
        {
            mockLibroService.Setup(service => service.GetLibroById(It.IsAny<int>())).Returns((Libro)null);

            var result = controller.GetLibro(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetLibro_ReturnsOkResult_ConLibro()
        {
            var libro = new Libro { Id = 1, Titulo = "Test", Autor = "Señor Paul", AnioPublicacion = "1965", Categoria = "XMen", Copias = 3 };
            mockLibroService.Setup(service => service.GetLibroById(It.IsAny<int>())).Returns(libro);

            var result = controller.GetLibro(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedLibro = Assert.IsType<Libro>(okResult.Value);
            Assert.Equal(1, returnedLibro.Id);
        }
    }
}
