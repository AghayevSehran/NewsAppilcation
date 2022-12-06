using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsApplication.Application.Contracts.Persistence;
using NewsApplication.Application.Contracts.Service;
using NewsApplication.Persistence.Repositories;
using NewsApplication.Persistence.Services;

namespace NewsApplication.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NewsDbContext>(options =>
        options.UseSqlServer(
               configuration.GetConnectionString("NewsDbConnectionString")));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IBulkCopyRepository<>), typeof(BulkCopyRepository<>));
        services.AddScoped(typeof(IPageService<,>), typeof(PageService<,>));
        return services;
    }
}
