using Microsoft.EntityFrameworkCore;
using OpenPeer.Domain.Entities;

namespace OpenPeer.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Article> Articles => Set<Article>(); 
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<EditorialDecision> EditorialDecisions => Set<EditorialDecision>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).IsRequired().HasMaxLength(150);
            e.Property(x => x.Abstract).IsRequired().HasMaxLength(4000);
            e.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            e.Property(x => x.RowVersion).IsRowVersion();
            e.HasIndex(x => x.Title);

            e.HasMany(x => x.Reviews)
             .WithOne(r => r.Article)
             .HasForeignKey(r => r.ArticleId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.EditorialDecision)
             .WithOne(d => d.Article)
             .HasForeignKey<EditorialDecision>(d => d.ArticleId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Review>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Reviewer).IsRequired().HasMaxLength(200);
            e.Property(x => x.Score).IsRequired();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<EditorialDecision>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Decision).HasConversion<string>().HasMaxLength(16);
            e.Property(x => x.DecidedAt).HasDefaultValueSql("GETUTCDATE()");
        });
    }
}
