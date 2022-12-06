using Microsoft.Extensions.Configuration;
using NewsApplication.Application.Contracts.Persistence;
using System.Data.SqlClient;

namespace NewsApplication.Persistence;

public class BulkCopyRepository<T> : IBulkCopyRepository<T> where T : class
{
    private readonly IConfiguration _configuration;
    public BulkCopyRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SaveBulk<T>(IEnumerable<T> data, string tableName)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:NewsDbConnectionString"]))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
            {
                bulkCopy.BatchSize = 100;
                bulkCopy.DestinationTableName = tableName;
                #region column mapping
                SqlBulkCopyColumnMapping Title =
                 new SqlBulkCopyColumnMapping("Title", "Title");
                bulkCopy.ColumnMappings.Add(Title);

                SqlBulkCopyColumnMapping Url =
               new SqlBulkCopyColumnMapping("Url", "Url");
                bulkCopy.ColumnMappings.Add(Url);

                SqlBulkCopyColumnMapping Content =
               new SqlBulkCopyColumnMapping("Content", "Content");
                bulkCopy.ColumnMappings.Add(Content);

                SqlBulkCopyColumnMapping NewsSource =
               new SqlBulkCopyColumnMapping("NewsSource", "NewsSource");
                bulkCopy.ColumnMappings.Add(NewsSource);

                SqlBulkCopyColumnMapping UrlToImage =
                 new SqlBulkCopyColumnMapping("UrlToImage", "UrlToImage");
                bulkCopy.ColumnMappings.Add(UrlToImage);

                SqlBulkCopyColumnMapping Description =
                new SqlBulkCopyColumnMapping("Description", "Description");
                bulkCopy.ColumnMappings.Add(Description);

                SqlBulkCopyColumnMapping PublishedAt =
                new SqlBulkCopyColumnMapping("PublishedAt", "PublishedAt");
                bulkCopy.ColumnMappings.Add(PublishedAt);

                SqlBulkCopyColumnMapping Author =
                new SqlBulkCopyColumnMapping("Author", "Author");
                bulkCopy.ColumnMappings.Add(Author);
                #endregion
                try
                {                  
                    await bulkCopy.WriteToServerAsync(data.AsDataTable());
                }
                catch 
                {
                    transaction.Rollback();
                    throw;
                }
            }
            transaction.Commit();
        }
    }
}
