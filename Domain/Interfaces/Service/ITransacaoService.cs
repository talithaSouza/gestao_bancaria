using Domain.Entidades;
using Domain.Enums;

namespace Domain.Interfaces.Service
{
    public interface ITransacaoService
    {
        Task<Conta> ExecutarSaquesAsync(TipoTransacao tipoTransacao, int numeroConta, decimal saldoRetirado);
    }
}