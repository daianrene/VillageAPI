using System.Linq.Expressions;

namespace VillageAPI.Repository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
