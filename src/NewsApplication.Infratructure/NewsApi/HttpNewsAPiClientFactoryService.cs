using Microsoft.Extensions.Options;
using NewsApplication.Application.Contracts.Infrastructure.NewsApi;
using NewsApplication.Application.DTOs;
using NewsApplication.Application.Models;
using System.Text.Json;

namespace NewsApplication.Infratructure.NewsApi;

public class NewsApiHttpClientService : INewsApiHttpClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<NewsApiSettings> _optionsSettings;
    private readonly JsonSerializerOptions _options;
    public NewsApiHttpClientService(IHttpClientFactory httpClientFactory, IOptions<NewsApiSettings> options)
    {
        _httpClientFactory = httpClientFactory;
        _optionsSettings = options;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<NewsApiResponse> Execute()
    {
        return await GetNews();
    }
    private async Task<NewsApiResponse> GetNews()
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.Timeout = TimeSpan.FromSeconds(_optionsSettings.Value.ApiTimeOut);
        using (var response = await httpClient.GetAsync(_optionsSettings.Value.ApiUrl))
        {
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<NewsApiResponse>(stream, _options);
        }
    }
}
