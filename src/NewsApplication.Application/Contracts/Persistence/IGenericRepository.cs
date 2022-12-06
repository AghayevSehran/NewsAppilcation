namespace NewsApplication.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<T> Get(int id);
    Task<IReadOnlyList<T>> GetAll();
    IQueryable<T> GetQuerable();
    Task<T> Add(T entity);
    Task<bool> Exists(int id);
    Task Update(T entity);
    Task Delete(T entity);
}