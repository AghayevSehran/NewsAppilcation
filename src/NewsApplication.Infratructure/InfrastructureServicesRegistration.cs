using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsApplication.Application.Contracts.Infrastructure.NewsApi;
using NewsApplication.Application.Contracts.Persistence;
using NewsApplication.Application.Models;
using NewsApplication.Infratructure.NewsApi;

namespace NewsApplication.Infratructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.Configure<NewsApiSettings>(configuration.GetSection("NewsApiSettings"));
        services.AddTransient<INewsApiHttpClientService, NewsApiHttpClientService>();
        return services;
    }
}
