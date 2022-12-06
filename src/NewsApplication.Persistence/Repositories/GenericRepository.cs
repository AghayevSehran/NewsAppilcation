using Microsoft.EntityFrameworkCore;
using NewsApplication.Application.Contracts.Persistence;

namespace NewsApplication.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly NewsDbContext _dbContext;

    public GenericRepository(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> Add(T entity)
    {
        await _dbContext.AddAsync(entity);
        return entity;
    }

    public async Task Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await Get(id);
        return entity != null;
    }

    public async Task<T> Get(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public IQueryable<IReadOnlyList<T>> GetPagedList(IQueryable<T> ordered, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetQuerable()
    {
        return _dbContext.Set<T>();
    }

    public async Task Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

}
