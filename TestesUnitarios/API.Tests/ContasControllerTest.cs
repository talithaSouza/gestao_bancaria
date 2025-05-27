using System.Net;
using System.Threading.Tasks;
using API.Controllers;
using Domain.Entidades;
using Domain.Exceptions;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestesUnitarios.API.Tests
{
    public class ContasControllerTest
    {
        private readonly Mock<IContaService> _serviceMock;
        private ContasController _controller;
        private Mock<IUrlHelper> _urlHelperMock;

        public ContasControllerTest()
        {
            _serviceMock = new Mock<IContaService>();
            _urlHelperMock = new Mock<IUrlHelper>();

            _controller = new ContasController(_serviceMock.Object);

            _urlHelperMock.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                          .Returns("http://localhost/api/contas/");

            _controller.Url = _urlHelperMock.Object;
        }

        #region CriarAsync
        [Fact(DisplayName = "Criando conta com sucesso")]
        public async Task CriandoConta_Status201()
        {
            var novaConta = new Conta(Faker.RandomNumber.Next(100, 200), 100);

            _serviceMock.Setup(m => m.CriarAsync(novaConta)).ReturnsAsync(novaConta);

            var result = await _controller.Criar(novaConta);

            var resultType = Assert.IsType<CreatedResult>(result);
        }

        [Fact(DisplayName = "Criando conta - Conflito")]
        public async Task CriandoConta_Conflito409()
        {
            var novaConta = new Conta(Faker.RandomNumber.Next(100, 200), 100);

            _serviceMock.Setup(m => m.CriarAsync(novaConta))
                        .ThrowsAsync(new ConflitoContaException("conta já existe"));

            var result = await _controller.Criar(novaConta);

            var resultType = Assert.IsType<ObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.Conflict, resultType.StatusCode);
        }

        [Fact(DisplayName = "Criando contas - BadRequest")]
        public async Task CriandoConta_400_DomainException()
        {
            var novaConta = new Conta(Faker.RandomNumber.Next(100, 200), 100);

            _serviceMock.Setup(m => m.CriarAsync(novaConta))
                        .ThrowsAsync(new DomainException("Erro de preenchimento"));

            var result = await _controller.Criar(novaConta);

            var resultType = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Criando contas - InternalServerErro")]
        public async Task CriandoConta_500_ExceptionGenerica()
        {
            var novaConta = new Conta(Faker.RandomNumber.Next(100, 200), 100);

            _serviceMock.Setup(m => m.CriarAsync(novaConta))
                        .ThrowsAsync(new Exception("Erro interno"));

            var result = await _controller.Criar(novaConta);

            var resultType = Assert.IsType<ObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.InternalServerError, resultType.StatusCode);
        }

        #endregion

        #region RetornarConta
        [Fact(DisplayName = "Retornar conta com sucesso")]
        public async Task RetornarConta_Status201()
        {
            int numeroConta = Faker.RandomNumber.Next(100, 200);

            _serviceMock.Setup(m => m.RetornarAsync(numeroConta)).ReturnsAsync(new Conta(numeroConta, 10));

            var result = await _controller.RetornarConta(numeroConta);

            var resultType = Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Retornar conta - Notfound")]
        public async Task RetornarConta_404()
        {
            int numeroConta = Faker.RandomNumber.Next(100, 200);

            _serviceMock.Setup(m => m.RetornarAsync(numeroConta))
                        .ThrowsAsync(new NotFoundException("Não encontrado"));

            var result = await _controller.RetornarConta(numeroConta);

            var resultType = Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact(DisplayName = "Retornar conta - InternalServerErro")]
        public async Task RetornarConta_500_ExceptionGenerica()
        {
            int numeroConta = Faker.RandomNumber.Next(100, 200);

            _serviceMock.Setup(m => m.RetornarAsync(numeroConta))
                        .ThrowsAsync(new Exception("Não encontrado"));

            var result = await _controller.RetornarConta(numeroConta);

            var resultType = Assert.IsType<ObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.InternalServerError, resultType.StatusCode);
        }
        #endregion
    }
}