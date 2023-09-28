using System.Linq.Expressions;

namespace AppHotel.Domain.RepositoryContracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity, string? id = null);

        Task<List<T>> GetByAsync(Expression<Func<T, bool>> filter);

        Task<T> DeleteByAsync(string? id);

        Task<long> DeleteManyByAsync(Expression<Func<T, bool>> filter);
    }
}
