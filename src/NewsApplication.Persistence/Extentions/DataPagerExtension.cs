using Microsoft.EntityFrameworkCore;
using NewsApplication.Application.Models;
using System.Linq.Expressions;

namespace NewsApplication.Persistence.Extentions;

public static class DataPagerExtension
{
    public static async Task<PagedModel<TModel>> PaginateAsync<TModel,TKey>(
        this IQueryable<TModel> query,
        Expression<Func<TModel, TKey>> predicate,
        int page,
        int limit,
        CancellationToken cancellationToken)
        where TModel : class
    {

        var paged = new PagedModel<TModel>();
        page = (page < 0) ? 1 : page;
        paged.CurrentPage = page;
        paged.PageSize = limit;
        paged.TotalItems = await query.CountAsync(cancellationToken); 
        var startRow = (page - 1) * limit;
        paged.Items = await query
                        .OrderBy(predicate)
                        .Skip(startRow)
                        .Take(limit)
                        .ToListAsync(cancellationToken);
        paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

        return paged;
    }
}
