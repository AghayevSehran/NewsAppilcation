using Microsoft.AspNetCore.Builder;
using NewsApplication.API.Middleware;
using NewsApplication.Application;
using NewsApplication.Infratructure;
using NewsApplication.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "News.Api v1"));
}

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
