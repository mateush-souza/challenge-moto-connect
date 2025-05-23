namespace challenge_moto_connect.Infrastructure.Persistence.Repositories
{
    public interface IRepository<T> where T: class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
