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
            return (await _entities.FindAsync(filter)).ToList();
        }

        public async Task UpdateAsync(T entity, string? id = null)
        {
            if (entity.Id == null)
                entity.Id = id;

            await _entities.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<T> DeleteByAsync(string? id)
        {
            return await _entities.FindOneAndDeleteAsync(id);
        }

        public async Task<long> DeleteManyByAsync(Expression<Func<T, bool>> filter)
        {
            return (await _entities.DeleteManyAsync(filter)).DeletedCount;
        }
    }
}
