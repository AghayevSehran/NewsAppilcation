using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NewsApplication.Domain;

namespace NewsApplication.Persistence.Configurations.Entities;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder
         .HasKey(b => b.Id)
         .HasName("PrimaryKey_ArticleId");

        builder
         .Property(f => f.Id)
         .ValueGeneratedOnAdd();

        builder
        .OwnsOne(c => c.NewsSource, options => { options.ToJson(); });
        
        builder
         .Property(b => b.UrlToImage)
         .IsRequired(false);

        builder
         .Property(b => b.Title)
         .IsRequired(false);

        builder
         .Property(b => b.Url)
         .IsRequired(false);

        builder
         .Property(b => b.Author)
         .IsRequired(false);

        builder
         .Property(b => b.Description)
         .IsRequired(false);

        builder
         .Property(b => b.Content)
         .IsRequired(false);

              

        builder
         .Property(b => b.DateCreated)
         .HasDefaultValueSql("getdate()");

        builder
         .Property(b => b.LastModifiedDate)
         .HasDefaultValueSql("getdate()");

    }
}

