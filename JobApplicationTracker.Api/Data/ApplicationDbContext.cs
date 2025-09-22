using Microsoft.EntityFrameworkCore;
using JobApplicationTracker.Api.Models;

namespace JobApplicationTracker.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<JobApplication> JobApplications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Company)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Status)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(e => e.DateApplied)
                .IsRequired();

           
            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();
        });

        // Seed some sample data
        modelBuilder.Entity<JobApplication>().HasData(
            new JobApplication
            {
                Id = 1,
                Company = "Microsoft",
                Position = "Software Engineer",
                Status = ApplicationStatus.Applied,
                DateApplied = DateTime.UtcNow.AddDays(-5),
               
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new JobApplication
            {
                Id = 2,
                Company = "Google",
                Position = "Senior Software Engineer",
                Status = ApplicationStatus.Interview,
                DateApplied = DateTime.UtcNow.AddDays(-10),
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            }
        );
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is JobApplication && (
                e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            var entity = (JobApplication)entityEntry.Entity;

            if (entityEntry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}