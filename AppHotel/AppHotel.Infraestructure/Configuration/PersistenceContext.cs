using AppHotel.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AppHotel.Infraestructure.Configuration
{
    public class PersistenceContext
    {
        public readonly IMongoDatabase mongoDatabase;

        public PersistenceContext(IOptions<Database> dataBase)
        {
            MongoClient mongoClient = new(dataBase.Value.Connection);
            mongoDatabase = mongoClient.GetDatabase(dataBase.Value.Name);
        }
    }
}
