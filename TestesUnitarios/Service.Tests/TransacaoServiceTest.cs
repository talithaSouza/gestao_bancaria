using Domain.Entidades;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Moq;
using Services;
using Sprache;

namespace TestesUnitarios.Service.Tests
{
    public class TransacaoServiceTest
    {
        private readonly Mock<IContaRepository> _contaRepositoryMock;
        private readonly Mock<ITransacaoRepository> _repositoryMock;

        private readonly ITransacaoService _service;

        public TransacaoServiceTest()
        {
            _contaRepositoryMock = new Mock<IContaRepository>();
            _repositoryMock = new Mock<ITransacaoRepository>();

            _service = new TransacaoService(_repositoryMock.Object, _contaRepositoryMock.Object);
        }

        [Fact(DisplayName = "ExecutarSaquesAsync para o tipo PIX")]
        public async Task ExecutarSaquesAsync_TipoPIX()
        {
            Conta contaSalva = new(Faker.RandomNumber.Next(100, 200), 100);

            int numeroConta = contaSalva.NumeroConta;
            decimal valorRetirado = 10;

            decimal saldoRestante = contaSalva.Saldo - valorRetirado;

            _contaRepositoryMock.Setup(m => m.Retornar(numeroConta)).ReturnsAsync(contaSalva);

            _repositoryMock.Setup(m => m.AtualizarSaldoAsync(It.IsAny<Conta>())).ReturnsAsync((Conta contaSaldoAlterado) => contaSaldoAlterado);

            var result = await _service.ExecutarSaquesAsync(Domain.Enums.TipoTransacao.P, numeroConta, valorRetirado);

            Assert.NotNull(result);
            Assert.Equal(numeroConta, result.NumeroConta);
            Assert.Equal(saldoRestante, result.Saldo);
        }
        
        [Fact(DisplayName = "ExecutarSaquesAsync para o tipo DEBITO")]
        public async Task ExecutarSaquesAsync_TipoDEBITO()
        {
            Conta contaSalva = new(Faker.RandomNumber.Next(100, 200), 100);

            int numeroConta = contaSalva.NumeroConta;
            decimal valorRetirado = 10;
            decimal taxaOperacao = 0.03m;

            decimal saldoRestante = contaSalva.Saldo - (valorRetirado + (valorRetirado * taxaOperacao));

            _contaRepositoryMock.Setup(m => m.Retornar(numeroConta)).ReturnsAsync(contaSalva);

            _repositoryMock.Setup(m => m.AtualizarSaldoAsync(It.IsAny<Conta>())).ReturnsAsync((Conta contaSaldoAlterado) => contaSaldoAlterado);

            var result = await _service.ExecutarSaquesAsync(Domain.Enums.TipoTransacao.D, numeroConta, valorRetirado);

            Assert.NotNull(result);
            Assert.Equal(numeroConta, result.NumeroConta);
            Assert.Equal(saldoRestante, result.Saldo);
        }
        
        [Fact(DisplayName = "ExecutarSaquesAsync para o tipo CREDITO")]
        public async Task ExecutarSaquesAsync_TipoCREDITO()
        {
            Conta contaSalva = new(Faker.RandomNumber.Next(100, 200), 100);

            int numeroConta = contaSalva.NumeroConta;
            decimal valorRetirado = 10;
            decimal taxaOperacao = 0.05m;

            decimal saldoRestante = contaSalva.Saldo - (valorRetirado + (valorRetirado * taxaOperacao));

            _contaRepositoryMock.Setup(m => m.Retornar(numeroConta)).ReturnsAsync(contaSalva);

            _repositoryMock.Setup(m => m.AtualizarSaldoAsync(It.IsAny<Conta>())).ReturnsAsync((Conta contaSaldoAlterado) => contaSaldoAlterado);

            var result = await _service.ExecutarSaquesAsync(Domain.Enums.TipoTransacao.C, numeroConta, valorRetirado);

            Assert.NotNull(result);
            Assert.Equal(numeroConta, result.NumeroConta);
            Assert.Equal(saldoRestante, result.Saldo);
        }
        
    }
}