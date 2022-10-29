using System.Linq.Expressions;
using DemoStore.Services.QuerySide.Common;
using MongoDB.Driver;

namespace DemoStore.Services.QuerySide.Infrastructure.MongoDb;

internal class MongoRepository<TDocument> : IRepository<TDocument> where TDocument : IDocument
{
    protected readonly IMongoCollection<TDocument> Collection;

    public MongoRepository(IMongoDatabase database)
    {
        Collection = database.GetCollection<TDocument>(typeof(TDocument).Name.ToLowerInvariant());
    }

    public async Task<IReadOnlyList<TDocument>> GetAllAsync(CancellationToken token = default)
    {
        var result = await Collection.FindAsync(_ => true, cancellationToken: token);
        return await result.ToListAsync(token);
    }

    public async Task<IReadOnlyList<TDocument>> GetAllAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken token = default)
    {
        var result = await Collection.FindAsync(predicate, cancellationToken: token);
        return await result.ToListAsync(token);
    }

    public async Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken token = default)
    {
        var result = await Collection.FindAsync(predicate, cancellationToken: token);
        return await result.FirstOrDefaultAsync(token);
    }

    public async Task<TDocument> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var result = await Collection.FindAsync(d => d.Id == id, cancellationToken: token);
        return await result.FirstOrDefaultAsync(token);
    }

    public async Task<TDocument> AddAsync(TDocument document, CancellationToken token = default)
    {
        var options = new InsertOneOptions
        {
            BypassDocumentValidation = false
        };

        await Collection.InsertOneAsync(document, options, token);
        return document;
    }

    public async Task<IReadOnlyList<TDocument>> AddRangeAsync(IEnumerable<TDocument> documents, CancellationToken token = default)
    {
        var options = new InsertManyOptions
        {
            BypassDocumentValidation = false
        };

        var docs = documents.ToList();

        await Collection.InsertManyAsync(docs, options, token);

        return docs;
    }

    public async Task<TDocument> UpdateAsync(TDocument document, CancellationToken token = default)
    {
        var replaceOptions = new ReplaceOptions
        {
            IsUpsert = false,
        };

        await Collection.ReplaceOneAsync(d => d.Id == document.Id, document, replaceOptions, token);
        return document;
    }

    public async Task DeleteAsync(Guid id, CancellationToken token = default)
    {
        await Collection.DeleteOneAsync(d => d.Id == id, token);
    }

    public async Task DeleteAsync(TDocument document, CancellationToken token = default)
    {
        await Collection.DeleteOneAsync(d => d.Id == document.Id, token);
    }
}