using Application.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data
{
	public class MongoDbContext
	{
		private readonly IMongoDatabase _database;
        private readonly MongoDBSettings _mongoDBSettings;
        public MongoDbContext(IOptions<MongoDBSettings> mongoDBSettings)
		{
            _mongoDBSettings = mongoDBSettings.Value;
			var client = new MongoClient(_mongoDBSettings.ConnectionString);
			_database = client.GetDatabase(_mongoDBSettings.DatabaseName);
		}

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

    }
}

