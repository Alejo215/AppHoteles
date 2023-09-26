using System.Linq.Expressions;

namespace AppHotel.Domain.RepositoryContracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task<List<T>> GetByAsync(Expression<Func<T, bool>> filter);
    }
}
