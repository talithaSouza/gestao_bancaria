using Domain.Entidades;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Moq;
using Services;

namespace TestesUnitarios.Service.Tests
{
    public class TransacaoServiceTest
    {
        private readonly Mock<IContaRepository> _contaRepositoryMock;
        private readonly Mock<ITransacaoRepository> _repositoryMock;
        private readonly Mock<IMovimentacaoService> _movimentacaoServiceMock;

        private readonly ITransacaoService _service;

        public TransacaoServiceTest()
        {
            _contaRepositoryMock = new Mock<IContaRepository>();
            _repositoryMock = new Mock<ITransacaoRepository>();
            _movimentacaoServiceMock = new Mock<IMovimentacaoService>();

            _service = new TransacaoService(_repositoryMock.Object, _contaRepositoryMock.Object, _movimentacaoServiceMock.Object);
        }

        [Fact(DisplayName = "ExecutarSaquesAsync para o tipo PIX")]
        public async Task ExecutarSaquesAsync_TipoPIX()
        {
            var tipoTransacao = Domain.Enums.TipoTransacao.P;
            Conta contaSalva = new(Faker.RandomNumber.Next(100, 200), 100);

            int numeroConta = contaSalva.NumeroConta;
            decimal valorRetirado = 10;

            decimal saldoRestante = contaSalva.Saldo - valorRetirado;

            _contaRepositoryMock.Setup(m => m.Retornar(numeroConta)).ReturnsAsync(contaSalva);

            _repositoryMock.Setup(m => m.AtualizarSaldoAsync(It.IsAny<Conta>())).ReturnsAsync((Conta contaSaldoAlterado) => contaSaldoAlterado);

            _movimentacaoServiceMock
            .Setup(m => m.RegistrarAsync(numeroConta, It.IsAny<decimal>(), It.IsAny<decimal>(), tipoTransacao))
            .Returns(Task.CompletedTask);

            var result = await _service.ExecutarSaquesAsync(tipoTransacao, numeroConta, valorRetirado);

            _movimentacaoServiceMock.Verify(m => m.RegistrarAsync(numeroConta, It.IsAny<decimal>(), It.IsAny<decimal>(), tipoTransacao), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(numeroConta, result.NumeroConta);
            Assert.Equal(saldoRestante, result.Saldo);
        }

        [Fact(DisplayName = "ExecutarSaquesAsync para o tipo DEBITO")]
        public async Task ExecutarSaquesAsync_TipoDEBITO()
        {
            var tipoTransacao = Domain.Enums.TipoTransacao.D;
            Conta contaSalva = new(Faker.RandomNumber.Next(100, 200), 100);

            int numeroConta = contaSalva.NumeroConta;
            decimal valorRetirado = 10;
            decimal taxaOperacao = 0.03m;

            decimal saldoRestante = contaSalva.Saldo - (valorRetirado + (valorRetirado * taxaOperacao));

            _contaRepositoryMock.Setup(m => m.Retornar(numeroConta)).ReturnsAsync(contaSalva);

            _repositoryMock.Setup(m => m.AtualizarSaldoAsync(It.IsAny<Conta>())).ReturnsAsync((Conta contaSaldoAlterado) => contaSaldoAlterado);

            _movimentacaoServiceMock
            .Setup(m => m.RegistrarAsync(numeroConta, It.IsAny<decimal>(), It.IsAny<decimal>(), tipoTransacao))
            .Returns(Task.CompletedTask);

            var result = await _service.ExecutarSaquesAsync(tipoTransacao, numeroConta, valorRetirado);

            _movimentacaoServiceMock.Verify(m => m.RegistrarAsync(numeroConta, It.IsAny<decimal>(), It.IsAny<decimal>(), tipoTransacao), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(numeroConta, result.NumeroConta);
            Assert.Equal(saldoRestante, result.Saldo);
        }

        [Fact(DisplayName = "ExecutarSaquesAsync para o tipo CREDITO")]
        public async Task ExecutarSaquesAsync_TipoCREDITO()
        {
            var tipoTransacao = Domain.Enums.TipoTransacao.C;
            Conta contaSalva = new(Faker.RandomNumber.Next(100, 200), 100);

            int numeroConta = contaSalva.NumeroConta;
            decimal valorRetirado = 10;
            decimal taxaOperacao = 0.05m;

            decimal saldoRestante = contaSalva.Saldo - (valorRetirado + (valorRetirado * taxaOperacao));

            _contaRepositoryMock.Setup(m => m.Retornar(numeroConta)).ReturnsAsync(contaSalva);

            _repositoryMock.Setup(m => m.AtualizarSaldoAsync(It.IsAny<Conta>())).ReturnsAsync((Conta contaSaldoAlterado) => contaSaldoAlterado);

            _movimentacaoServiceMock
            .Setup(m => m.RegistrarAsync(numeroConta, It.IsAny<decimal>(), It.IsAny<decimal>(),tipoTransacao))
            .Returns(Task.CompletedTask);

            var result = await _service.ExecutarSaquesAsync(tipoTransacao, numeroConta, valorRetirado);

            _movimentacaoServiceMock.Verify(m => m.RegistrarAsync(numeroConta, It.IsAny<decimal>(), It.IsAny<decimal>(),tipoTransacao), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(numeroConta, result.NumeroConta);
            Assert.Equal(saldoRestante, result.Saldo);
        }

    }
}