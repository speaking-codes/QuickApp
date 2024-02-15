using DAL.Models.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryNoSql.Interfaces
{
    public interface IRepositoryNoSql<TDocument> where TDocument : class, IDocument
    {
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filter);

        Task<TDocument> FindOneAsync(FilterDefinition<TDocument> filter);

        TDocument FindOne(Expression<Func<TDocument, bool>> filter);

        TDocument FindOne(FilterDefinition<TDocument> filter);

        Task InsertOneAsync(TDocument document);

        void InsertOne(TDocument document, bool insertTimeToLive=true, int timeToLiveSeconds=7200);

        Task InsertManyAsync(IEnumerable<TDocument> documents);

        void InsertMany(IEnumerable<TDocument> documents);

        Task<bool> UpdateOneAsync(TDocument document, UpdateDefinition<TDocument> definition);

        bool UpdateOne(TDocument document, UpdateDefinition<TDocument> definition);

        Task<bool> UpdateManyAsync(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null);

        Task<bool> UpdateManyAsync(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null);

        bool UpdateMany(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null);

        bool UpdateMany(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, UpdateOptions updateOptions = null);

        Task<TDocument> FindAndUpdateOneAsync(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, FindOneAndUpdateOptions<TDocument, TDocument> findOneAndUpdateOptions = null);

        Task<TDocument> FindAndUpdateOneAsync(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, FindOneAndUpdateOptions<TDocument, TDocument> findOneAndUpdateOptions = null);

        TDocument FindAndUpdateOne(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, FindOneAndUpdateOptions<TDocument, TDocument> findOneAndUpdateOptions = null);

        TDocument FindAndUpdateOne(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, FindOneAndUpdateOptions<TDocument, TDocument> findOneAndUpdateOptions = null);

        Task<TProjection> ProjectOneAsync<TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
                    where TProjection : class;

        Task<TProjection> ProjectOneAsync<TProjection>(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument, TProjection> projection)
            where TProjection : class;

        TProjection ProjectOne<TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TProjection : class;

        TProjection ProjectOne<TProjection>(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument, TProjection> projection)
            where TProjection : class;

        Task<IList<TProjection>> ProjectManyAsync<TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TProjection : class;

        Task<IList<TProjection>> ProjectManyAsync<TProjection>(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument, TProjection> projection)
            where TProjection : class;

        IList<TProjection> ProjectMany<TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TProjection : class;

        IList<TProjection> ProjectMany<TProjection>(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument, TProjection> projection)
            where TProjection : class;

        Task<long> CountAsync(Expression<Func<TDocument, bool>> filter);

        Task<long> CountAsync(FilterDefinition<TDocument> filter);

        int Count();

        long Count(Expression<Func<TDocument, bool>> filter);

        long Count(FilterDefinition<TDocument> filter);

    }
}
