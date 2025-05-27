using System.Threading.Tasks;
using Domain.Entidades;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Moq;
using Service.Services;

namespace TestesUnitarios.Service.Tests
{
    public class ContasServiceTest
    {
        private Mock<IContaRepository> _repositoryMock;

        private IContaService _service;

        public ContasServiceTest()
        {
            _repositoryMock = new Mock<IContaRepository>();

            _service = new ContaService(_repositoryMock.Object);
        }

        #region CriarAsync
        [Fact(DisplayName = "CriarContaService com sucesso")]
        public async Task CriarContaComSucesso()
        {
            int numeroConta = Faker.RandomNumber.Next(100, 200);
            decimal saldo = Faker.RandomNumber.Next(100);

            _repositoryMock.Setup(m => m.Retornar(numeroConta)).Returns(Task.FromResult((Conta)null));

            _repositoryMock.Setup(m => m.Criar(It.IsAny<Conta>())).ReturnsAsync((Conta contaSalva) => contaSalva);

            var result = await _service.CriarAsync(new Conta(numeroConta, saldo));

            Assert.NotNull(result);
            Assert.Equal(numeroConta, result.NumeroConta);
            Assert.Equal(saldo, result.Saldo);
        }


        [Fact(DisplayName = "CriarContaService - ConflitoException")]
        public async Task CriarConta_ConflitoException()
        {
            int numeroConta = Faker.RandomNumber.Next(100, 200);
            decimal saldo = Faker.RandomNumber.Next(100);

            _repositoryMock.Setup(m => m.Retornar(numeroConta)).ReturnsAsync(new Conta(numeroConta, 0));

            var result = await Assert.ThrowsAsync<ConflitoContaException>(() => _service.CriarAsync(new Conta(numeroConta, saldo)));

            Assert.NotNull(result);
            Assert.Equal($"Já existe uma conta cadastrada com a numeração {numeroConta}", result.Message);

            _repositoryMock.Verify(m => m.Criar(It.IsAny<Conta>()), Times.Never);
        }
        #endregion

        #region RetornarAsync
        [Fact(DisplayName = "RetornaContaService com sucesso")]
        public async Task RetornarContaComSucesso()
        {
            int numeroConta = Faker.RandomNumber.Next(100, 200);
            decimal saldo = Faker.RandomNumber.Next(100);

            _repositoryMock.Setup(m => m.Retornar(numeroConta)).ReturnsAsync(new Conta(numeroConta, saldo));

            var result = await _service.RetornarAsync(numeroConta);

            Assert.NotNull(result);
            Assert.Equal(numeroConta, result.NumeroConta);
            Assert.Equal(saldo, result.Saldo);
        }

        [Fact(DisplayName = "RetornaContaService Não Encontrada")]
        public async Task RetornarContaNotFoundException()
        {
            int numeroConta = Faker.RandomNumber.Next(100, 200);
            decimal saldo = Faker.RandomNumber.Next(100);

            _repositoryMock.Setup(m => m.Retornar(numeroConta)).Returns(Task.FromResult((Conta)null));

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _service.RetornarAsync(numeroConta));

            Assert.NotNull(result);
            Assert.Equal($"Não foi encontrada uma conta com o número {numeroConta}", result.Message);
        }
        #endregion
    }
}