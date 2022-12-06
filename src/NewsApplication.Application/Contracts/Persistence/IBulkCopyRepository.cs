using System.Data;

namespace NewsApplication.Application.Contracts.Persistence;

public interface IBulkCopyRepository<T> where T : class
{
    Task SaveBulk<T>(IEnumerable<T> data,string tableName);
}
