using System.Linq.Expressions;

namespace DemoStore.Services.QuerySide.Common;

public interface IRepository<TDocument> where TDocument : IDocument
{
    Task<IReadOnlyList<TDocument>> GetAllAsync(CancellationToken token = default);
    Task<IReadOnlyList<TDocument>> GetAllAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken token = default);

    Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken token = default);
    Task<TDocument> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<TDocument> AddAsync(TDocument document, CancellationToken token = default);
    Task<IReadOnlyList<TDocument>> AddRangeAsync(IEnumerable<TDocument> documents, CancellationToken token = default);

    Task<TDocument> UpdateAsync(TDocument document, CancellationToken token = default);

    Task DeleteAsync(Guid id, CancellationToken token = default);
    Task DeleteAsync(TDocument document, CancellationToken token = default);
}