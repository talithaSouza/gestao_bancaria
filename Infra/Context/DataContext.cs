using Microsoft.EntityFrameworkCore;

namespace Infra.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}