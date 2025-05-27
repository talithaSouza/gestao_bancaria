using System.Net;
using System.Threading.Tasks;
using API.Controllers;
using API.DTO;
using AutoMapper;
using Domain.Entidades;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit.Sdk;

namespace TestesUnitarios.API.Tests
{
    public class TransacoesControllerTest: BaseMapperTest
    {
        private readonly Mock<ITransacaoService> _serviceMock;
        private TransacoesController _controller;
        private Mock<IUrlHelper> _urlHelperMock;
        private readonly IMapper _mapper;

        private readonly Mock<IMovimentacaoService> _movimentacaoServiceMock;
        public TransacoesControllerTest()
        {
            _serviceMock = new Mock<ITransacaoService>();
            _urlHelperMock = new Mock<IUrlHelper>();
            _mapper = Mapper;
            _movimentacaoServiceMock = new Mock<IMovimentacaoService>();

            _controller = new TransacoesController(_serviceMock.Object, _movimentacaoServiceMock.Object, _mapper);

            _urlHelperMock.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                          .Returns("http://localhost/api/transacoes/");

            _controller.Url = _urlHelperMock.Object;
        }

        #region RealizarTransacao
        [Theory(DisplayName = "Realizando Transações com sucesso")]
        [InlineData(TipoTransacao.C)]
        [InlineData(TipoTransacao.D)]
        [InlineData(TipoTransacao.P)]
        public async Task Transacoes_Status201(TipoTransacao tipoTransacao)
        {
            var contaAlterada = new ContaDTO()
            {
                NumeroConta =  Faker.RandomNumber.Next(100, 200),
                Saldo =  10
            };

            _serviceMock.Setup(m => m.ExecutarSaquesAsync(tipoTransacao, contaAlterada.NumeroConta, contaAlterada.Saldo))
                        .ReturnsAsync(new Conta(contaAlterada.NumeroConta, contaAlterada.Saldo));

            var result = await _controller.RealizarTransacao(tipoTransacao, contaAlterada);

            var resultType = Assert.IsType<CreatedResult>(result);
        }

        [Fact(DisplayName = "Realizando Transações - Saldo Insuficiente")]
        public async Task Transacoes_Status404_SaldoInsuficiente()
        {
            var contaAlterada = new ContaDTO()
            {
                NumeroConta =  Faker.RandomNumber.Next(100, 200),
                Saldo =  10
            };

            TipoTransacao tipoTransacaoRandom = GetTipoTransacaoAleatoria();

            _serviceMock.Setup(m => m.ExecutarSaquesAsync(tipoTransacaoRandom, contaAlterada.NumeroConta, contaAlterada.Saldo))
                        .ThrowsAsync(new SaldoInsificienteException());

            var result = await _controller.RealizarTransacao(tipoTransacaoRandom, contaAlterada);

            var resultType = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact(DisplayName = "Realizando Transações - Conta Não Encontrada")]
        public async Task Transacoes_Status404_ContaNaoEncontrada()
        {
            var contaAlterada = new ContaDTO()
            {
                NumeroConta =  Faker.RandomNumber.Next(100, 200),
                Saldo =  10
            };

            TipoTransacao tipoTransacaoRandom = GetTipoTransacaoAleatoria();

            _serviceMock.Setup(m => m.ExecutarSaquesAsync(tipoTransacaoRandom, contaAlterada.NumeroConta, contaAlterada.Saldo))
                        .ThrowsAsync(new NotFoundException());

            var result = await _controller.RealizarTransacao(tipoTransacaoRandom, contaAlterada);

            var resultType = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact(DisplayName = "Realizando Transações - InvalidOperationException")]
        public async Task Transacoes_Status400_InvalidOperationException()
        {
            var contaAlterada = new ContaDTO()
            {
                NumeroConta =  Faker.RandomNumber.Next(100, 200),
                Saldo =  10
            };
            
            TipoTransacao tipoTransacaoRandom = GetTipoTransacaoAleatoria();

            _serviceMock.Setup(m => m.ExecutarSaquesAsync(tipoTransacaoRandom, contaAlterada.NumeroConta, contaAlterada.Saldo))
                        .ThrowsAsync(new InvalidOperationException());

            var result = await _controller.RealizarTransacao(tipoTransacaoRandom, contaAlterada);

            var resultType = Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region BuscarMovimentacoesDaConta
        [Fact(DisplayName = "Controller Buscar Movimentacoes por conta")]
        public async Task BuscarMovimentacaoPorContaComSucesso()
        {
            _movimentacaoServiceMock
            .Setup(m => m.RetornarMovimentacoesPorConta(It.IsAny<int>()))
            .ReturnsAsync(new List<Movimentacao>());

            var result = await _controller.BuscarMovimentacoesDaConta(Faker.RandomNumber.Next());

            Assert.IsType<OkObjectResult>(result);
        }


        [Fact(DisplayName = "Controller Buscar Movimentacoes por conta Exception")]
        public async Task BuscarMovimentacaoPorContaException()
        {
            _movimentacaoServiceMock
            .Setup(m => m.RetornarMovimentacoesPorConta(It.IsAny<int>()))
            .ThrowsAsync(new Exception());

            var result = await _controller.BuscarMovimentacoesDaConta(Faker.RandomNumber.Next());

            var resultStatus = Assert.IsType<ObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.InternalServerError, resultStatus.StatusCode);
        }
        #endregion

        private static TipoTransacao GetTipoTransacaoAleatoria()
        {
            var valores = Enum.GetValues(typeof(TipoTransacao));
            return (TipoTransacao)valores.GetValue(new Random().Next(valores.Length));
        }
    }
}