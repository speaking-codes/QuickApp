using DAL.Models.Interfaces;
using DAL.MongoDB;
using DAL.RepositoryNoSql.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryNoSql
{
    public class RepositoryNoSql<TDocument> : IRepositoryNoSql<TDocument> where TDocument : class, IDocument
    {
        protected IMongoDbContext Context { get; }

        protected string CollectionName { get; }

        protected RepositoryNoSql(IMongoDbContext context, string collectionName)
        {
            Context = context;
            CollectionName = collectionName;
        }

        public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filter)
        {
            return await GetCollection().Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task<TDocument> FindOneAsync(FilterDefinition<TDocument> filter)
        {
            return await GetCollection().Find(filter).FirstOrDefaultAsync();
        }

        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filter)
        {
            return GetCollection().Find(filter).FirstOrDefault();
        }

        public virtual TDocument FindOne(FilterDefinition<TDocument> filter)
        {
            return GetCollection().Find(filter).FirstOrDefault();
        }

        public virtual async Task InsertOneAsync(TDocument document)
        {
            await GetCollection().InsertOneAsync(document);
        }

        public virtual void InsertOne(TDocument document)
        {
            GetCollection().InsertOne(document);
        }

        public virtual async Task InsertManyAsync(IEnumerable<TDocument> documents)
        {
            await GetCollection().InsertManyAsync(documents);
        }

        public virtual void InsertMany(IEnumerable<TDocument> documents)
        {
            GetCollection().InsertMany(documents);
        }

        public virtual async Task<bool> UpdateOneAsync(TDocument document, UpdateDefinition<TDocument> definition)
        {
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, document.Id);
            var updateResult = await GetCollection().UpdateOneAsync(filter, definition, new UpdateOptions() { IsUpsert = true });
            return updateResult.ModifiedCount == 1;
        }

        public virtual bool UpdateOne(TDocument document, UpdateDefinition<TDocument> definition)
        {
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, document.Id);
            var updateResult = GetCollection().UpdateOne(filter, definition, new UpdateOptions() { IsUpsert = true });
            return updateResult.ModifiedCount == 1;
        }

        public virtual async Task<bool> UpdateManyAsync(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null)
        {
            var updateResult = await GetCollection().UpdateManyAsync(filter, updateDefinition, updateOptions);
            return updateResult.ModifiedCount > 0;
        }

        public virtual async Task<bool> UpdateManyAsync(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null)
        {
            var updateResult = await GetCollection().UpdateManyAsync(filter, updateDefinition, updateOptions);
            return updateResult.ModifiedCount > 0;
        }

        public virtual bool UpdateMany(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null)
        {
            var updateResult = GetCollection().UpdateMany(filter, updateDefinition, updateOptions);
            return updateResult.ModifiedCount > 0;
        }

        public virtual bool UpdateMany(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null)
        {
            var updateResult = GetCollection().UpdateMany(filter, updateDefinition, updateOptions);
            return updateResult.ModifiedCount > 0;
        }

        public virtual async Task<TDocument> FindAndUpdateOneAsync(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, FindOneAndUpdateOptions<TDocument, TDocument> findOneAndUpdateOptions = null)
        {
            return await GetCollection().FindOneAndUpdateAsync(filter, updateDefinition, findOneAndUpdateOptions);
        }

        public virtual async Task<TDocument> FindAndUpdateOneAsync(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, FindOneAndUpdateOptions<TDocument, TDocument> findOneAndUpdateOptions = null)
        {
            return await GetCollection().FindOneAndUpdateAsync(filter, updateDefinition, findOneAndUpdateOptions);
        }

        public virtual TDocument FindAndUpdateOne(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, FindOneAndUpdateOptions<TDocument, TDocument> findOneAndUpdateOptions = null)
        {
            return GetCollection().FindOneAndUpdate(filter, updateDefinition, findOneAndUpdateOptions);
        }

        public virtual TDocument FindAndUpdateOne(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, FindOneAndUpdateOptions<TDocument, TDocument> findOneAndUpdateOptions = null)
        {
            return GetCollection().FindOneAndUpdate(filter, updateDefinition, findOneAndUpdateOptions);
        }

        public virtual async Task<TProjection> ProjectOneAsync<TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TProjection : class
        {
            return await GetCollection().Find(filter).Project(projection).FirstOrDefaultAsync();
        }

        public virtual async Task<TProjection> ProjectOneAsync<TProjection>(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument, TProjection> projection)
            where TProjection : class
        {
            return await GetCollection().Find(filter).Project(projection).FirstOrDefaultAsync();
        }

        public virtual TProjection ProjectOne<TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TProjection : class
        {
            return GetCollection().Find(filter).Project(projection).FirstOrDefault();
        }

        public virtual TProjection ProjectOne<TProjection>(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument, TProjection> projection)
            where TProjection : class
        {
            return GetCollection().Find(filter).Project(projection).FirstOrDefault();
        }

        public virtual async Task<IList<TProjection>> ProjectManyAsync<TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TProjection : class
        {
            return await GetCollection().Find(filter).Project(projection).ToListAsync();
        }

        public virtual async Task<IList<TProjection>> ProjectManyAsync<TProjection>(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument, TProjection> projection)
            where TProjection : class
        {
            return await GetCollection().Find(filter).Project(projection).ToListAsync();
        }

        public virtual IList<TProjection> ProjectMany<TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TProjection : class
        {
            return GetCollection().Find(filter).Project(projection).ToList();
        }

        public virtual IList<TProjection> ProjectMany<TProjection>(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument, TProjection> projection)
            where TProjection : class
        {
            return GetCollection().Find(filter).Project(projection).ToList();
        }

        public virtual async Task<long> CountAsync(Expression<Func<TDocument, bool>> filter)
        {
            return await GetCollection().Find(filter).CountDocumentsAsync();
        }

        public virtual async Task<long> CountAsync(FilterDefinition<TDocument> filter)
        {
            return await GetCollection().Find(filter).CountDocumentsAsync();
        }

        public virtual int Count()
        {
            return GetCollection().AsQueryable().Count();
        }

        public virtual long Count(Expression<Func<TDocument, bool>> filter)
        {
            return GetCollection().Find(filter).CountDocuments();
        }

        public virtual long Count(FilterDefinition<TDocument> filter)
        {
            return GetCollection().Find(filter).CountDocuments();
        }

        protected IMongoCollection<TDocument> GetCollection()
        {
            return Context.GetCollection<TDocument>(CollectionName);
        }   
        
        public virtual DeleteResult DeleteOne(FilterDefinition<TDocument> filter)
        {
            return GetCollection().DeleteOne(filter);
        }

        public virtual async Task<DeleteResult> DeleteOneAsync(FilterDefinition<TDocument> filter)
        {
            return await GetCollection().DeleteOneAsync(filter);
        }
    }
}
