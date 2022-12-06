using NewsAplication.Background;
using NewsApplication.Application;
using NewsApplication.Infratructure;
using NewsApplication.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.AddScoped<SampleService>();
builder.Services.AddSingleton<NewsService>();
builder.Services.AddHostedService(
    provider => provider.GetRequiredService<NewsService>());
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Getting data from server once a day!");
app.MapGet("/background", (
    NewsService service) =>
{
    return new NewsServiceState(service.IsEnabled);
});
app.MapMethods("/background", new[] { "PATCH" }, (
    NewsServiceState state,
    NewsService service) =>
{
    service.IsEnabled = state.IsEnabled;
});
app.Run();

