using System.Linq.Expressions;
using Application.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
	public class MongoRepository<T>: IRepository<T> where T: class
	{
        private readonly IMongoCollection<T> _mongoCollection;

		public MongoRepository(IMongoDatabase database, string collectionName)
		{
            _mongoCollection = database.GetCollection<T>(collectionName);
		}

        public async Task<T> AddAsync(T entity)
        {
            await _mongoCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            var results = await _mongoCollection.DeleteOneAsync(filter);

            if (results.DeletedCount > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _mongoCollection.Find(doc => true).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _mongoCollection.Find(predicate).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var results = await _mongoCollection.ReplaceOneAsync(filter, entity);

            if (results.MatchedCount > 0)
            {
                return true;
            }
            return false;
        }
    }
}

