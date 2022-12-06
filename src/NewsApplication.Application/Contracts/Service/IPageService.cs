using NewsApplication.Application.DTOs;
using NewsApplication.Application.DTOs.Common;
using NewsApplication.Application.Models;
using System.Linq.Expressions;

namespace NewsApplication.Application.Contracts.Service;

public interface IPageService<T,TKey> where T : class
{
    Task<PagedModel<T>> GetPagedList(IQueryable<T> query, Expression<Func<T, TKey>> predicate, PagingDto pagingRequest, CancellationToken cancellationToken = default);
}
