using Moq;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using WebAppLib1.Interfaces;
using WebAppLib1.Controllers;
using WebAppLib1.Models;

namespace WebAppLib1.Test
{
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioService> mockUsuarioService;
        private readonly UsuarioController controller;

        public UsuarioControllerTest()
        {
            this.mockUsuarioService = new Mock<IUsuarioService>();
            this.controller = new UsuarioController(mockUsuarioService.Object);
        }

        [Fact]
        public void GetUsuarios_ReturnsOKResult_ConListaDeUsuarios()
        {
            mockUsuarioService.Setup(s => s.GetAllUsuarios()).Returns(new List<Usuario>());

            var result = controller.GetUsuarios();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<Usuario>>(okResult.Value);
        }

        [Fact]
        public void GetUsuario_ReturnsNotFoundResult_CuandoUsuarioNoExiste()
        {
            mockUsuarioService.Setup(service => service.GetUsuarioById(It.IsAny<int>())).Returns((Usuario)null);

            var result = controller.GetUsuario(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetUsuario_ReturnsOkResult_ConUsuario()
        {
            var usuario = new Usuario { Id = 1, Username = "Test", Email = "test@ksp.net", Password = "eey123@complete!" };
            mockUsuarioService.Setup(service => service.GetUsuarioById(It.IsAny<int>())).Returns(usuario);

            var result = controller.GetUsuario(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsuario = Assert.IsType<Usuario>(okResult.Value);
            Assert.Equal(1, returnedUsuario.Id);
        }
    }
}
