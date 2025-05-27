using System.Security.Cryptography.X509Certificates;
using Domain.Entidades;

namespace Domain.Interfaces.Repository
{
    public interface IMovimentacaoRepository
    {
        Task<Movimentacao> Inserir(Movimentacao movimentacaoSaldo);
        Task<List<Movimentacao>> RetornaTodasMovimentacoesPorConta(int numeroConta);

    }
}