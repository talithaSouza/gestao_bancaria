using Domain.Entidades;
using Domain.Enums;

namespace Domain.Interfaces.Service
{
    public interface IMovimentacaoService
    {
        Task RegistrarAsync(int numeroConta, decimal saldoAntes, decimal saldoDepois, TipoTransacao tipoTransacao);
        Task<List<Movimentacao>> RetornarMovimentacoesPorConta(int numeroConta);
    }
}