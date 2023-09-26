using AppHotel.Domain.Entities;
using AppHotel.Domain.RepositoryContracts;
using AppHotel.Infraestructure.Configuration;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace AppHotel.Infraestructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseDocument
    {
        private readonly IMongoCollection<T> _entities;

        public BaseRepository(PersistenceContext context)
        {
            _entities = context.mongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public async Task AddAsync(T entity)
        {
            await _entities.InsertOneAsync(entity);
        }

        public async Task<List<T>> GetByAsync(Expression<Func<T, bool>> filter)
        {
            return await _entities.Find(filter).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _entities.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}
