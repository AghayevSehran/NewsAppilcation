using AutoMapper;
using Azure.Core;
using NewsApplication.Application.Contracts.Infrastructure.NewsApi;
using NewsApplication.Application.Contracts.Persistence;
using NewsApplication.Application.DTOs;
using NewsApplication.Application.DTOs.Article;
using NewsApplication.Domain;
using Newtonsoft.Json;

namespace NewsAplication.Background;
public class NewsService : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(15);
    private readonly ILogger<NewsService> _logger;
    private readonly IServiceScopeFactory _factory;
    private int _executionCount = 0;
    public bool IsEnabled { get; set; }

    public NewsService(
        ILogger<NewsService> logger,
        IServiceScopeFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        while (
            !stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (IsEnabled)
                {
                    await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                    var httmlClient = asyncScope.ServiceProvider.GetRequiredService<INewsApiHttpClientService>();
                    var articleRepository = asyncScope.ServiceProvider.GetRequiredService<IBulkCopyRepository<Article>>();
                    var result = await httmlClient.Execute();
                    var dtoArticles = result.Articles.Select(c => new ArticleSqlBulkCopyDto()
                    {
                        NewsSource = JsonConvert.SerializeObject(c.Source),
                        Author = c.Author,
                        Content = c.Content,
                        Description = c.Description,
                        PublishedAt = c.PublishedAt,
                        Title = c.Title,
                        Url = c.Url,
                        UrlToImage = c.UrlToImage
                    }) .ToList();

                    await articleRepository.SaveBulk(dtoArticles, "dbo.Articles");
                    _logger.LogInformation(
                        $"Executed PeriodicHostedService - Count: {_executionCount}");
                }
                else
                {
                    _logger.LogInformation(
                        "Skipped PeriodicHostedService");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(
                    $"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
            }
        }
    }
}