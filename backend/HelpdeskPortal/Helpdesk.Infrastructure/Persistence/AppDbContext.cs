using Helpdesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(t => t.Description)
                  .IsRequired()
                  .HasMaxLength(2000);

            entity.Property(t => t.Category)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(t => t.CreatedByEmail)
                  .IsRequired()
                  .HasMaxLength(150);

            // Store enums as readable strings in SQL Server instead of raw integers
            entity.Property(t => t.Status)
                  .HasConversion<string>()
                  .HasMaxLength(30);

            entity.Property(t => t.Priority)
                  .HasConversion<string>()
                  .HasMaxLength(30);
        });
    }
}