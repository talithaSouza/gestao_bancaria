using Domain.Entidades;

namespace Domain.Interfaces.Service
{
    public interface ITransacaoService
    {
        Task<Conta> Pix(int numeroConta, decimal saldoRetirado);
    }
}