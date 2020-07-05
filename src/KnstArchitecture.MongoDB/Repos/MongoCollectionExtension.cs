using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KnstArchitecture.Repos
{
    public static class MongoCollectionExctension
    {
        #region Aggregate
        public static IAggregateFluent<TDocument> Aggregate<TDocument>(this IMongoCollection<TDocument> collection, AggregateOptions options, IClientSessionHandle session)
        {
            if (session == null)
                return collection.Aggregate<TDocument>(options);
            else
                return collection.Aggregate<TDocument>(session, options);
        }
        public static IAsyncCursor<TResult> Aggregate<TDocument, TResult>(this IMongoCollection<TDocument> collection, PipelineDefinition<TDocument, TResult> pipeline, IClientSessionHandle session, AggregateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.Aggregate<TResult>(pipeline, options, cancellationToken);
            else
                return collection.Aggregate<TResult>(session, pipeline, options, cancellationToken);
        }
        #endregion
        #region AggregateAsync
        public async static Task<IAsyncCursor<TResult>> AggregateAsync<TDocument, TResult>(this IMongoCollection<TDocument> collection, PipelineDefinition<TDocument, TResult> pipeline, IClientSessionHandle session, AggregateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.AggregateAsync<TResult>(pipeline, options, cancellationToken);
            else
                return await collection.AggregateAsync<TResult>(session, pipeline, options, cancellationToken);
        }
        #endregion
        #region BulkWrite
        public static BulkWriteResult<TDocument> BulkWrite<TDocument>(this IMongoCollection<TDocument> collection, IEnumerable<WriteModel<TDocument>> requests, IClientSessionHandle session, BulkWriteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.BulkWrite(requests, options, cancellationToken);
            else
                return collection.BulkWrite(session, requests, options, cancellationToken);
        }
        #endregion
        #region BulkWriteAsync
        public static async Task<BulkWriteResult<TDocument>> BulkWriteAsync<TDocument>(this IMongoCollection<TDocument> collection, IEnumerable<WriteModel<TDocument>> requests, IClientSessionHandle session, BulkWriteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.BulkWriteAsync(requests, options, cancellationToken);
            else
                return await collection.BulkWriteAsync(session, requests, options, cancellationToken);
        }
        #endregion
        #region Count
        [Obsolete("Use CountDocuments or EstimatedDocumentCount instead.")]
        public static long Count<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.Count<TDocument>(filter, options, cancellationToken);
            else
                return collection.Count<TDocument>(session, filter, options, cancellationToken);
        }

        [Obsolete("Use CountDocuments or EstimatedDocumentCount instead.")]
        public static long Count<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.Count(filter, options, cancellationToken);
            else
                return collection.Count(session, filter, options, cancellationToken);
        }
        #endregion
        #region CountAsync
        [Obsolete("Use CountDocumentsAsync or EstimatedDocumentCountAsync instead.")]
        public static async Task<long> CountAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.CountAsync<TDocument>(filter, options, cancellationToken);
            else
                return await collection.CountAsync<TDocument>(session, filter, options, cancellationToken);
        }

        [Obsolete("Use CountDocumentsAsync or EstimatedDocumentCountAsync instead.")]
        public static async Task<long> CountAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.CountAsync(filter, options, cancellationToken);
            else
                return await collection.CountAsync(session, filter, options, cancellationToken);
        }
        #endregion
        #region CountDocuments
        public static long CountDocuments<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.CountDocuments<TDocument>(filter, options, cancellationToken);
            else
                return collection.CountDocuments<TDocument>(session, filter, options, cancellationToken);
        }
        public static long CountDocuments<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.CountDocuments(filter, options, cancellationToken);
            else
                return collection.CountDocuments(session, filter, options, cancellationToken);
        }
        #endregion
        #region CountDocumentsAsync
        public static async Task<long> CountDocumentsAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.CountDocumentsAsync<TDocument>(filter, options, cancellationToken);
            else
                return await collection.CountDocumentsAsync<TDocument>(session, filter, options, cancellationToken);
        }
        public static async Task<long> CountDocumentsAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.CountDocumentsAsync(filter, options, cancellationToken);
            else
                return await collection.CountDocumentsAsync(session, filter, options, cancellationToken);
        }
        #endregion
        #region DeleteMany
        public static DeleteResult DeleteMany<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, DeleteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.DeleteMany<TDocument>(filter, options, cancellationToken);
            else
                return collection.DeleteMany<TDocument>(session, filter, options, cancellationToken);
        }
        public static DeleteResult DeleteMany<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, DeleteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.DeleteMany(filter, options, cancellationToken);
            else
                return collection.DeleteMany(session, filter, options, cancellationToken);
        }
        #endregion
        #region DeleteManyAsync
        public static async Task<DeleteResult> DeleteManyAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, DeleteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.DeleteManyAsync<TDocument>(filter, options, cancellationToken);
            else
                return await collection.DeleteManyAsync<TDocument>(session, filter, options, cancellationToken);
        }
        public static async Task<DeleteResult> DeleteManyAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, DeleteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.DeleteManyAsync(filter, options, cancellationToken);
            else
                return await collection.DeleteManyAsync(session, filter, options, cancellationToken);
        }
        #endregion
        #region DeleteOne
        public static DeleteResult DeleteOne<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, DeleteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.DeleteOne<TDocument>(filter, options, cancellationToken);
            else
                return collection.DeleteOne<TDocument>(session, filter, options, cancellationToken);
        }
        public static DeleteResult DeleteOne<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, DeleteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.DeleteOne(filter, options, cancellationToken);
            else
                return collection.DeleteOne(session, filter, options, cancellationToken);
        }
        #endregion
        #region DeleteOneAsync
        public static async Task<DeleteResult> DeleteOneAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, DeleteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.DeleteOneAsync<TDocument>(filter, options, cancellationToken);
            else
                return await collection.DeleteOneAsync<TDocument>(session, filter, options, cancellationToken);
        }
        public static async Task<DeleteResult> DeleteOneAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, DeleteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.DeleteOneAsync(filter, options, cancellationToken);
            else
                return await collection.DeleteOneAsync(session, filter, options, cancellationToken);
        }
        #endregion
        #region Distinct
        public static IAsyncCursor<TField> Distinct<TDocument, TField>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, TField>> field, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.Distinct<TDocument, TField>(field, filter, options, cancellationToken);
            else
                return collection.Distinct<TDocument, TField>(session, field, filter, options, cancellationToken);
        }
        public static IAsyncCursor<TField> Distinct<TDocument, TField>(this IMongoCollection<TDocument> collection, FieldDefinition<TDocument, TField> field, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.Distinct<TDocument, TField>(field, filter, options, cancellationToken);
            else
                return collection.Distinct<TDocument, TField>(session, field, filter, options, cancellationToken);
        }
        public static IAsyncCursor<TField> Distinct<TDocument, TField>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, TField>> field, FilterDefinition<TDocument> filter, IClientSessionHandle session, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.Distinct<TDocument, TField>(field, filter, options, cancellationToken);
            else
                return collection.Distinct<TDocument, TField>(session, field, filter, options, cancellationToken);
        }
        public static IAsyncCursor<TField> Distinct<TDocument, TField>(this IMongoCollection<TDocument> collection, FieldDefinition<TDocument, TField> field, FilterDefinition<TDocument> filter, IClientSessionHandle session, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.Distinct<TField>(field, filter, options, cancellationToken);
            else
                return collection.Distinct<TField>(session, field, filter, options, cancellationToken);
        }
        #endregion
        #region DistinctAsync
        public static async Task<IAsyncCursor<TField>> DistinctAsync<TDocument, TField>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, TField>> field, FilterDefinition<TDocument> filter, IClientSessionHandle session, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.DistinctAsync<TDocument, TField>(field, filter, options, cancellationToken);
            else
                return await collection.DistinctAsync<TDocument, TField>(session, field, filter, options, cancellationToken);
        }
        public static async Task<IAsyncCursor<TField>> DistinctAsync<TDocument, TField>(this IMongoCollection<TDocument> collection, FieldDefinition<TDocument, TField> field, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.DistinctAsync<TDocument, TField>(field, filter, options, cancellationToken);
            else
                return await collection.DistinctAsync<TDocument, TField>(session, field, filter, options, cancellationToken);
        }
        public static async Task<IAsyncCursor<TField>> DistinctAsync<TDocument, TField>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, TField>> field, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.DistinctAsync<TDocument, TField>(field, filter, options, cancellationToken);
            else
                return await collection.DistinctAsync<TDocument, TField>(session, field, filter, options, cancellationToken);
        }
        public static async Task<IAsyncCursor<TField>> DistinctAsync<TDocument, TField>(this IMongoCollection<TDocument> collection, FieldDefinition<TDocument, TField> field, FilterDefinition<TDocument> filter, IClientSessionHandle session, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.DistinctAsync<TField>(field, filter, options, cancellationToken);
            else
                return await collection.DistinctAsync<TField>(session, field, filter, options, cancellationToken);
        }
        #endregion
        #region Find
        public static IFindFluent<TDocument, TDocument> Find<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, FindOptions options = null)
        {
            if (session == null)
                return collection.Find<TDocument>(filter, options);
            else
                return collection.Find<TDocument>(session, filter, options);
        }
        public static IFindFluent<TDocument, TDocument> Find<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, FindOptions options = null)
        {
            if (session == null)
                return collection.Find<TDocument>(filter, options);
            else
                return collection.Find<TDocument>(session, filter, options);
        }
        #endregion
        #region FindAsync
        public static async Task<IAsyncCursor<TDocument>> FindAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, FindOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindAsync<TDocument>(filter, options, cancellationToken);
            else
                return await collection.FindAsync<TDocument>(session, filter, options, cancellationToken);
        }
        public static async Task<IAsyncCursor<TDocument>> FindAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, FindOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindAsync<TDocument>(filter, options, cancellationToken);
            else
                return await collection.FindAsync<TDocument>(session, filter, options, cancellationToken);
        }
        public static async Task<IAsyncCursor<TProjection>> FindAsync<TDocument, TProjection>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, FindOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindAsync<TProjection>(filter, options, cancellationToken);
            else
                return await collection.FindAsync<TProjection>(session, filter, options, cancellationToken);
        }
        #endregion
        #region FindOneAndDelete
        public static TProjection FindOneAndDelete<TDocument, TProjection>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, FindOneAndDeleteOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndDelete<TDocument, TProjection>(filter, options, cancellationToken);
            else
                return collection.FindOneAndDelete<TDocument, TProjection>(session, filter, options, cancellationToken);
        }
        public static TDocument FindOneAndDelete<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, FindOneAndDeleteOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndDelete<TDocument>(filter, options, cancellationToken);
            else
                return collection.FindOneAndDelete<TDocument>(session, filter, options, cancellationToken);
        }
        public static TDocument FindOneAndDelete<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, FindOneAndDeleteOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndDelete<TDocument>(filter, options, cancellationToken);
            else
                return collection.FindOneAndDelete<TDocument>(session, filter, options, cancellationToken);
        }
        public static TProjection FindOneAndDelete<TDocument, TProjection>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, FindOneAndDeleteOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndDelete<TProjection>(filter, options, cancellationToken);
            else
                return collection.FindOneAndDelete<TProjection>(session, filter, options, cancellationToken);
        }
        #endregion
        #region FindOneAndDeleteAsync
        public static async Task<TDocument> FindOneAndDeleteAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, FindOneAndDeleteOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndDeleteAsync<TDocument>(filter, options, cancellationToken);
            else
                return await collection.FindOneAndDeleteAsync<TDocument>(session, filter, options, cancellationToken);
        }
        public static async Task<TDocument> FindOneAndDeleteAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, FindOneAndDeleteOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndDeleteAsync<TDocument>(filter, options, cancellationToken);
            else
                return await collection.FindOneAndDeleteAsync<TDocument>(session, filter, options, cancellationToken);
        }
        public static async Task<TProjection> FindOneAndDeleteAsync<TDocument, TProjection>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, FindOneAndDeleteOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndDeleteAsync<TDocument, TProjection>(filter, options, cancellationToken);
            else
                return await collection.FindOneAndDeleteAsync<TDocument, TProjection>(session, filter, options, cancellationToken);
        }
        public static async Task<TProjection> FindOneAndDeleteAsync<TDocument, TProjection>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, FindOneAndDeleteOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndDeleteAsync<TProjection>(filter, options, cancellationToken);
            else
                return await collection.FindOneAndDeleteAsync<TProjection>(session, filter, options, cancellationToken);
        }
        #endregion
        #region FindOneAndReplace
        public static TDocument FindOneAndReplace<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, TDocument replacement, IClientSessionHandle session, FindOneAndReplaceOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndReplace<TDocument>(filter, replacement, options, cancellationToken);
            else
                return collection.FindOneAndReplace<TDocument>(session, filter, replacement, options, cancellationToken);
        }
        public static TProjection FindOneAndReplace<TDocument, TProjection>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, TDocument replacement, IClientSessionHandle session, FindOneAndReplaceOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndReplace<TDocument, TProjection>(filter, replacement, options, cancellationToken);
            else
                return collection.FindOneAndReplace<TDocument, TProjection>(session, filter, replacement, options, cancellationToken);
        }
        public static TDocument FindOneAndReplace<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, TDocument replacement, IClientSessionHandle session, FindOneAndReplaceOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndReplace<TDocument>(filter, replacement, options, cancellationToken);
            else
                return collection.FindOneAndReplace<TDocument>(session, filter, replacement, options, cancellationToken);
        }
        public static TProjection FindOneAndReplace<TDocument, TProjection>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, TDocument replacement, IClientSessionHandle session, FindOneAndReplaceOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndReplace<TProjection>(filter, replacement, options, cancellationToken);
            else
                return collection.FindOneAndReplace<TProjection>(session, filter, replacement, options, cancellationToken);
        }
        #endregion
        #region FindOneAndReplaceAsync
        public static async Task<TDocument> FindOneAndReplaceAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, TDocument replacement, IClientSessionHandle session, FindOneAndReplaceOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndReplaceAsync<TDocument>(filter, replacement, options, cancellationToken);
            else
                return await collection.FindOneAndReplaceAsync<TDocument>(session, filter, replacement, options, cancellationToken);
        }
        public static async Task<TDocument> FindOneAndReplaceAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, TDocument replacement, IClientSessionHandle session, FindOneAndReplaceOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndReplaceAsync<TDocument>(filter, replacement, options, cancellationToken);
            else
                return await collection.FindOneAndReplaceAsync<TDocument>(filter, session, replacement, options, cancellationToken);
        }
        public static async Task<TProjection> FindOneAndReplaceAsync<TDocument, TProjection>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, TDocument replacement, IClientSessionHandle session, FindOneAndReplaceOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndReplaceAsync<TDocument, TProjection>(filter, replacement, options, cancellationToken);
            else
                return await collection.FindOneAndReplaceAsync<TDocument, TProjection>(session, filter, replacement, options, cancellationToken);
        }
        public static async Task<TProjection> FindOneAndReplaceAsync<TDocument, TProjection>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, TDocument replacement, IClientSessionHandle session, FindOneAndReplaceOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndReplaceAsync<TProjection>(filter, replacement, options, cancellationToken);
            else
                return await collection.FindOneAndReplaceAsync<TProjection>(session, filter, replacement, options, cancellationToken);
        }
        #endregion
        #region FindOneAndUpdate
        public static TDocument FindOneAndUpdate<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, FindOneAndUpdateOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndUpdate<TDocument>(filter, update, options, cancellationToken);
            else
                return collection.FindOneAndUpdate<TDocument>(session, filter, update, options, cancellationToken);
        }
        public static TDocument FindOneAndUpdate<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, FindOneAndUpdateOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndUpdate<TDocument>(filter, update, options, cancellationToken);
            else
                return collection.FindOneAndUpdate<TDocument>(session, filter, update, options, cancellationToken);
        }
        public static TProjection FindOneAndUpdate<TDocument, TProjection>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, FindOneAndUpdateOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndUpdate<TDocument, TProjection>(filter, update, options, cancellationToken);
            else
                return collection.FindOneAndUpdate<TDocument, TProjection>(session, filter, update, options, cancellationToken);
        }
        public static TProjection FindOneAndUpdate<TDocument, TProjection>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, FindOneAndUpdateOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindOneAndUpdate<TProjection>(filter, update, options, cancellationToken);
            else
                return collection.FindOneAndUpdate<TProjection>(session, filter, update, options, cancellationToken);
        }
        #endregion
        #region FindOneAndUpdateAsync
        public static async Task<TDocument> FindOneAndUpdateAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, FindOneAndUpdateOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndUpdateAsync<TDocument>(filter, update, options, cancellationToken);
            else
                return await collection.FindOneAndUpdateAsync<TDocument>(session, filter, update, options, cancellationToken);
        }
        public static async Task<TDocument> FindOneAndUpdateAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, FindOneAndUpdateOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndUpdateAsync<TDocument>(filter, update, options, cancellationToken);
            else
                return await collection.FindOneAndUpdateAsync<TDocument>(session, filter, update, options, cancellationToken);
        }
        public static async Task<TProjection> FindOneAndUpdateAsync<TDocument, TProjection>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, FindOneAndUpdateOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndUpdateAsync<TDocument, TProjection>(filter, update, options, cancellationToken);
            else
                return await collection.FindOneAndUpdateAsync<TDocument, TProjection>(session, filter, update, options, cancellationToken);
        }
        public static async Task<TProjection> FindOneAndUpdateAsync<TDocument, TProjection>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, FindOneAndUpdateOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.FindOneAndUpdateAsync<TProjection>(filter, update, options, cancellationToken);
            else
                return await collection.FindOneAndUpdateAsync<TProjection>(session, filter, update, options, cancellationToken);
        }
        #endregion
        #region FindSync
        public static IAsyncCursor<TDocument> FindSync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, IClientSessionHandle session, FindOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindSync<TDocument>(filter, options, cancellationToken);
            else
                return collection.FindSync<TDocument>(session, filter, options, cancellationToken);
        }
        public static IAsyncCursor<TDocument> FindSync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, FindOptions<TDocument, TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindSync<TDocument>(filter, options, cancellationToken);
            else
                return collection.FindSync<TDocument>(session, filter, options, cancellationToken);
        }
        public static IAsyncCursor<TProjection> FindSync<TDocument, TProjection>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, IClientSessionHandle session, FindOptions<TDocument, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.FindSync<TProjection>(filter, options, cancellationToken);
            else
                return collection.FindSync<TProjection>(session, filter, options, cancellationToken);
        }
        #endregion
        #region InsertMany
        public static void InsertMany<TDocument>(this IMongoCollection<TDocument> collection, IEnumerable<TDocument> documents, IClientSessionHandle session, InsertManyOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                collection.InsertMany(documents, options, cancellationToken);
            else
                collection.InsertMany(session, documents, options, cancellationToken);
        }
        #endregion
        #region InsertAsync
        public static async Task InsertManyAsync<TDocument>(this IMongoCollection<TDocument> collection, IEnumerable<TDocument> documents, IClientSessionHandle session, InsertManyOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                await collection.InsertManyAsync(documents, options, cancellationToken);
            else
                await collection.InsertManyAsync(session, documents, options, cancellationToken);
        }
        #endregion
        #region InsertOne
        public static void InsertOne<TDocument>(this IMongoCollection<TDocument> collection, TDocument document, IClientSessionHandle session, InsertOneOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                collection.InsertOne(document, options, cancellationToken);
            else
                collection.InsertOne(session, document, options, cancellationToken);
        }
        #endregion
        #region InsertOneAsync
        public static async Task InsertOneAsync<TDocument>(this IMongoCollection<TDocument> collection, TDocument document, IClientSessionHandle session, InsertOneOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                await collection.InsertOneAsync(document, options, cancellationToken);
            else
                await collection.InsertOneAsync(session, document, options, cancellationToken);
        }
        #endregion
        #region MapReduce
        public static IAsyncCursor<TResult> MapReduce<TDocument, TResult>(this IMongoCollection<TDocument> collection, BsonJavaScript map, BsonJavaScript reduce, IClientSessionHandle session, MapReduceOptions<TDocument, TResult> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.MapReduce<TResult>(map, reduce, options, cancellationToken);
            else
                return collection.MapReduce<TResult>(session, map, reduce, options, cancellationToken);
        }
        #endregion
        #region MapReduceAsync
        public static async Task<IAsyncCursor<TResult>> MapReduceAsync<TDocument, TResult>(this IMongoCollection<TDocument> collection, BsonJavaScript map, BsonJavaScript reduce, IClientSessionHandle session, MapReduceOptions<TDocument, TResult> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.MapReduceAsync<TResult>(map, reduce, options, cancellationToken);
            else
                return await collection.MapReduceAsync<TResult>(session, map, reduce, options, cancellationToken);
        }
        #endregion
        #region ReplaceOne
        [Obsolete("Use the overload that takes a ReplaceOptions instead of an UpdateOptions.")]
        public static ReplaceOneResult ReplaceOne<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, TDocument replacement, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.ReplaceOne<TDocument>(filter, replacement, options, cancellationToken);
            else
                return collection.ReplaceOne<TDocument>(session, filter, replacement, options, cancellationToken);
        }
        [Obsolete("Use the overload that takes a ReplaceOptions instead of an UpdateOptions.")]
        public static ReplaceOneResult ReplaceOne<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, TDocument replacement, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.ReplaceOne(filter, replacement, options, cancellationToken);
            else
                return collection.ReplaceOne(session, filter, replacement, options, cancellationToken);
        }
        #endregion
        #region ReplaceOneAsync
        [Obsolete("Use the overload that takes a ReplaceOptions instead of an UpdateOptions.")]
        public static async Task<ReplaceOneResult> ReplaceOneAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, TDocument replacement, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.ReplaceOneAsync<TDocument>(filter, replacement, options, cancellationToken);
            else
                return await collection.ReplaceOneAsync<TDocument>(session, filter, replacement, options, cancellationToken);
        }
        [Obsolete("Use the overload that takes a ReplaceOptions instead of an UpdateOptions.")]
        public static async Task<ReplaceOneResult> ReplaceOneAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, TDocument replacement, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.ReplaceOneAsync(filter, replacement, options, cancellationToken);
            else
                return await collection.ReplaceOneAsync(session, filter, replacement, options, cancellationToken);
        }
        #endregion
        #region UpdateMany
        public static UpdateResult UpdateMany<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.UpdateMany<TDocument>(filter, update, options, cancellationToken);
            else
                return collection.UpdateMany<TDocument>(session, filter, update, options, cancellationToken);
        }
        public static UpdateResult UpdateMany<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.UpdateMany(filter, update, options, cancellationToken);
            else
                return collection.UpdateMany(session, filter, update, options, cancellationToken);
        }
        #endregion
        #region UpdateManyAsync
        public static async Task<UpdateResult> UpdateManyAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.UpdateManyAsync<TDocument>(filter, update, options, cancellationToken);
            else
                return await collection.UpdateOneAsync<TDocument>(session, filter, update, options, cancellationToken);
        }
        public static async Task<UpdateResult> UpdateManyAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.UpdateManyAsync(filter, update, options, cancellationToken);
            else
                return await collection.UpdateOneAsync(session, filter, update, options, cancellationToken);
        }
        #endregion
        #region UpdateOne
        public static UpdateResult UpdateOne<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.UpdateOne<TDocument>(filter, update, options, cancellationToken);
            else
                return collection.UpdateOne<TDocument>(session, filter, update, options, cancellationToken);
        }
        public static UpdateResult UpdateOne<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.UpdateOne(filter, update, options, cancellationToken);
            else
                return collection.UpdateOne(session, filter, update, options, cancellationToken);
        }
        #endregion
        #region UpdateOneAsync
        public static async Task<UpdateResult> UpdateOneAsync<TDocument>(this IMongoCollection<TDocument> collection, Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.UpdateOneAsync<TDocument>(filter, update, options, cancellationToken);
            else
                return await collection.UpdateOneAsync<TDocument>(session, filter, update, options, cancellationToken);
        }
        public static async Task<UpdateResult> UpdateOneAsync<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, IClientSessionHandle session, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.UpdateOneAsync(filter, update, options, cancellationToken);
            else
                return await collection.UpdateOneAsync(session, filter, update, options, cancellationToken);
        }
        #endregion
        #region Watch
        public static IAsyncCursor<ChangeStreamDocument<TDocument>> WatchV2<TDocument>(this IMongoCollection<TDocument> collection, IClientSessionHandle session, ChangeStreamOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.Watch<TDocument>(options, cancellationToken);
            else
                return collection.Watch<TDocument>(session, options, cancellationToken);
        }
        public static IAsyncCursor<TResult> WatchV2<TDocument, TResult>(this IMongoCollection<TDocument> collection, PipelineDefinition<ChangeStreamDocument<TDocument>, TResult> pipeline, IClientSessionHandle session, ChangeStreamOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return collection.Watch<TResult>(pipeline, options, cancellationToken);
            else
                return collection.Watch<TResult>(session, pipeline, options, cancellationToken);
        }
        #endregion
        #region WatchAsync
        public static async Task<IAsyncCursor<ChangeStreamDocument<TDocument>>> WatchV2Async<TDocument>(this IMongoCollection<TDocument> collection, IClientSessionHandle session, ChangeStreamOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.WatchAsync<TDocument>(options, cancellationToken);
            else
                return await collection.WatchAsync<TDocument>(session, options, cancellationToken);
        }
        public static async Task<IAsyncCursor<TResult>> WatchV2Async<TDocument, TResult>(this IMongoCollection<TDocument> collection, PipelineDefinition<ChangeStreamDocument<TDocument>, TResult> pipeline, IClientSessionHandle session, ChangeStreamOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (session == null)
                return await collection.WatchAsync<TResult>(pipeline, options, cancellationToken);
            else
                return await collection.WatchAsync<TResult>(session, pipeline, options, cancellationToken);
        }
        #endregion
    }
}