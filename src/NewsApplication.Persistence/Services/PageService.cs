using NewsApplication.Application.Contracts.Service;
using NewsApplication.Application.DTOs.Common;
using NewsApplication.Application.Models;
using NewsApplication.Persistence.Extentions;
using System.Linq.Expressions;

namespace NewsApplication.Persistence.Services;

public class PageService<T, TKey> : IPageService<T, TKey> where T : class
{  
    public Task<PagedModel<T>> GetPagedList(IQueryable<T> query, Expression<Func<T, TKey>> predicate, PagingDto pagingRequest, CancellationToken cancellationToken)
    {
        return query.PaginateAsync(predicate,pagingRequest.PageNumber, pagingRequest.PageSize, cancellationToken); ;
    }
}
