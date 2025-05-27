namespace Domain.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<T> Criar(T obj);
        public Task<T> Editar(T obj);
        public Task<bool> Remover(int id);
        public Task<T> Retornar(int id);
        public Task<IEnumerable<T>> RetornarTodos();
        
    }
}