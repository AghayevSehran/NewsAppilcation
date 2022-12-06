using Microsoft.EntityFrameworkCore;
using NewsApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApplication.Persistence;

public class AuditableDbContext : DbContext
{
    public AuditableDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseDomainEntity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.LastModifiedDate = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }
        }

        var result = await base.SaveChangesAsync();

        return result;
    }
}

