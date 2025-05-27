using Domain.Interfaces.Repository;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _context;

        public BaseRepository(DataContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<T> Criar(T obj)
        {
            _context.Add(obj);

            await _context.SaveChangesAsync();

            return obj;
        }

        public async Task<T> Editar(T obj)
        {
            _context.ChangeTracker.Clear();

            _context.Entry(obj).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return obj;
        }

        public async Task<bool> Remover(int id)
        {
            var obj = await _context.Set<T>().FindAsync(id);

            if (obj == null)
                return false;

            _context.Remove(obj);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<T?> Retornar(int id)
        {
             return await _context.Set<T>().FindAsync(id);

        }

        public async Task<IEnumerable<T>> RetornarTodos()
        {
             return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

    }
}