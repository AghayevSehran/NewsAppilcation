using Microsoft.EntityFrameworkCore;
using NewsApplication.Domain;

namespace NewsApplication.Persistence;

public class NewsDbContext : AuditableDbContext
{
    public NewsDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NewsDbContext).Assembly);
    }
    public DbSet<Article> Articles { get; set; }

}

