using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MongoDB
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly MongoCollectionSettings _settings;

        public MongoDbContext(string connectionString, string databaseName)
        {
            _settings = new MongoCollectionSettings()
            {
                GuidRepresentation = GuidRepresentation.Standard
            };
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
        }

        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }

        public IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName)
        {
            return Database.GetCollection<TDocument>(collectionName, _settings);
        }
    }
}
