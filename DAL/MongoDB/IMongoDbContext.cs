using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MongoDB
{
    public interface IMongoDbContext
    {
        IMongoClient Client { get; }

        IMongoDatabase Database { get; }

        IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName);
    }
}
