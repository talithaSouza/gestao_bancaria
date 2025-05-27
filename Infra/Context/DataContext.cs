using Domain.Entidades;
using Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        #region DATA SETS
        //C
        public DbSet<Conta> Contas { get; set; }

        //M
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //C
            modelBuilder.ApplyConfiguration(new ContaMap());

            //M
            modelBuilder.ApplyConfiguration(new MovimentacaoMap());
        }

    }
}