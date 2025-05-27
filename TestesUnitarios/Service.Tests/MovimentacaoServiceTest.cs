using Domain.Entidades;
using Domain.Enums;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Moq;
using Service.Services;

namespace TestesUnitarios.Service.Tests
{
    public class MovimentacaoServiceTest
    {
        private readonly Mock<IMovimentacaoRepository> _repositoryMock;

        private readonly IMovimentacaoService _service;

        public MovimentacaoServiceTest()
        {
            _repositoryMock = new Mock<IMovimentacaoRepository>();

            _service = new MovimentacaoService(_repositoryMock.Object);
        }

        [Theory(DisplayName = "Cadastrar movimentação bancaria")]
        [InlineData(TipoTransacao.C)]
        [InlineData(TipoTransacao.D)]
        [InlineData(TipoTransacao.P)]
        public async Task InserirMovimentacao(TipoTransacao tipoTransacao)
        {
            int numeroConta = Faker.RandomNumber.Next();
            decimal saldoAntes = Faker.RandomNumber.Next();
            decimal saldoAtualizado = Faker.RandomNumber.Next();

            Movimentacao? movimentacaoSalva = null;

            _repositoryMock.Setup(m => m.Inserir(It.IsAny<Movimentacao>()))
                           .Callback<Movimentacao>(m => movimentacaoSalva = m)
                           .ReturnsAsync((Movimentacao m) => m);

            await _service.RegistrarAsync(numeroConta, saldoAntes, saldoAtualizado, tipoTransacao);

            Assert.NotNull(movimentacaoSalva);
            Assert.Equal(numeroConta, movimentacaoSalva.NumeroConta);
            Assert.Equal(saldoAntes, movimentacaoSalva.SaldoAntes);
            Assert.Equal(saldoAtualizado, movimentacaoSalva.SaldoDepois);
            Assert.Equal(tipoTransacao, movimentacaoSalva.TipoTransacao);
        }

        [Fact(DisplayName = "Retornando movimentações")]
        public async Task RetornandoMovimentacoes()
        {
            _repositoryMock.Setup(m => m.RetornaTodasMovimentacoesPorConta(It.IsAny<int>()))
                           .ReturnsAsync(new List<Movimentacao>());

            var result = await _service.RetornarMovimentacoesPorConta(Faker.RandomNumber.Next());

            Assert.NotNull(result);
        }
    }
}