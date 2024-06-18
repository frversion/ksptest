using Moq;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using WebAppLib1.Controllers;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;

namespace WebAppLib1.Test
{
    public class PrestamoControllerTests
    {
        private readonly Mock<IPrestamoService> mockPrestamoService;
        private readonly PrestamoController controller;

        public PrestamoControllerTests()
        {
            mockPrestamoService = new Mock<IPrestamoService>();
            controller = new PrestamoController(mockPrestamoService.Object);
        }

        [Fact]
        public void GetPrestamos_ReturnsOkResult_ConListaDePrestamos()
        {
            mockPrestamoService.Setup(service => service.GetAllPrestamos()).Returns(new List<Prestamo>());

            var result = controller.GetPrestamos();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<Prestamo>>(okResult.Value);
        }

        [Fact]
        public void GetPrestamo_ReturnsNotFoundResult_CuandoPrestamoNoExiste()
        {
            mockPrestamoService.Setup(service => service.GetPrestamoById(It.IsAny<int>())).Returns((Prestamo)null);

            var result = controller.GetPrestamo(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetPrestamo_ReturnsOkResult_ConPrestamo()
        {
            var prestamo = new Prestamo { Id = 1, UserId = 1, LibroId = 1 };
            mockPrestamoService.Setup(service => service.GetPrestamoById(It.IsAny<int>())).Returns(prestamo);

            var result = controller.GetPrestamo(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPrestamo = Assert.IsType<Prestamo>(okResult.Value);
            Assert.Equal(1, returnedPrestamo.Id);
        }

        [Fact]
        public void PrestarLibro_ReturnsBadRequest_CuandoLibroNoEstaDisponible()
        {
            var prestamoParams = new PrestamoParams
            {
                UserId = 1,
                LibroId = 1
            };

            mockPrestamoService.Setup(service => service.PrestarLibro(It.IsAny<PrestamoParams>())).Throws(new InvalidOperationException("El libro no existe o no hay copias disponibles."));

            var result = controller.PrestarLibro(prestamoParams);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("El libro no existe o no hay copias disponibles.", badRequestResult.Value);
        }

        [Fact]
        public void PrestarLibro_ReturnsOkResult()
        {
            var prestamoParams = new PrestamoParams
            {
                UserId = 1,
                LibroId = 1
            };
            mockPrestamoService.Setup(service => service.PrestarLibro(It.IsAny<PrestamoParams>())).Verifiable();

            var result = controller.PrestarLibro(prestamoParams);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DevolverLibro_ReturnsBadRequest_CuandoPrestamoInvalido()
        {
            mockPrestamoService.Setup(service => service.DevolverLibro(It.IsAny<int>())).Throws(new InvalidOperationException("El préstamo no existe o ya ha sido devuelto."));

            var result = controller.DevolverLibro(1);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("El préstamo no existe o ya ha sido devuelto.", badRequestResult.Value);
        }

        [Fact]
        public void DevolverLibro_ReturnsOkResult()
        {
            mockPrestamoService.Setup(service => service.DevolverLibro(It.IsAny<int>())).Verifiable();

            var result = controller.DevolverLibro(1);

            Assert.IsType<OkResult>(result);
        }
    }
}
