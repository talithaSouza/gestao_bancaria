using API.Controllers;
using Domain.Entidades;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestesUnitarios.API.Tests
{
    public class TransacoesControllerTest
    {
        private readonly Mock<ITransacaoService> _serviceMock;
        private TransacoesController _controller;
        private Mock<IUrlHelper> _urlHelperMock;

        public TransacoesControllerTest()
        {
            _serviceMock = new Mock<ITransacaoService>();
            _urlHelperMock = new Mock<IUrlHelper>();

            _controller = new TransacoesController(_serviceMock.Object);

            _urlHelperMock.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                          .Returns("http://localhost/api/transacoes/");

            _controller.Url = _urlHelperMock.Object;
        }

        [Theory(DisplayName = "Realizando Transações com sucesso")]
        [InlineData(TipoTransacao.C)]
        [InlineData(TipoTransacao.D)]
        [InlineData(TipoTransacao.P)]
        public async Task Transacoes_Status201(TipoTransacao tipoTransacao)
        {
            var contaAlterada = new Conta(Faker.RandomNumber.Next(100, 200), 10);

            _serviceMock.Setup(m => m.ExecutarSaquesAsync(tipoTransacao, contaAlterada.NumeroConta, contaAlterada.Saldo))
                        .ReturnsAsync(contaAlterada);

            var result = await _controller.RealizarTransacao(tipoTransacao, contaAlterada);

            var resultType = Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Realizando Transações - Saldo Insuficiente")]
        public async Task Transacoes_Status404_SaldoInsuficiente()
        {
            var contaAlterada = new Conta(Faker.RandomNumber.Next(100, 200), 10);
            TipoTransacao tipoTransacaoRandom = GetTipoTransacaoAleatoria();

            _serviceMock.Setup(m => m.ExecutarSaquesAsync(tipoTransacaoRandom, contaAlterada.NumeroConta, contaAlterada.Saldo))
                        .ThrowsAsync(new SaldoInsificienteException());

            var result = await _controller.RealizarTransacao(tipoTransacaoRandom, contaAlterada);

            var resultType = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact(DisplayName = "Realizando Transações - Conta Não Encontrada")]
        public async Task Transacoes_Status404_ContaNaoEncontrada()
        {
            var contaAlterada = new Conta(Faker.RandomNumber.Next(100, 200), 10);
            TipoTransacao tipoTransacaoRandom = GetTipoTransacaoAleatoria();

            _serviceMock.Setup(m => m.ExecutarSaquesAsync(tipoTransacaoRandom, contaAlterada.NumeroConta, contaAlterada.Saldo))
                        .ThrowsAsync(new NotFoundException());

            var result = await _controller.RealizarTransacao(tipoTransacaoRandom, contaAlterada);

            var resultType = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact(DisplayName = "Realizando Transações - InvalidOperationException")]
        public async Task Transacoes_Status400_InvalidOperationException()
        {
            var contaAlterada = new Conta(Faker.RandomNumber.Next(100, 200), 10);
            TipoTransacao tipoTransacaoRandom = GetTipoTransacaoAleatoria();

            _serviceMock.Setup(m => m.ExecutarSaquesAsync(tipoTransacaoRandom, contaAlterada.NumeroConta, contaAlterada.Saldo))
                        .ThrowsAsync(new InvalidOperationException());

            var result = await _controller.RealizarTransacao(tipoTransacaoRandom, contaAlterada);

            var resultType = Assert.IsType<BadRequestObjectResult>(result);
        }

        private static TipoTransacao GetTipoTransacaoAleatoria()
        {
            var valores = Enum.GetValues(typeof(TipoTransacao));
            return (TipoTransacao)valores.GetValue(new Random().Next(valores.Length));
        }
    }
}