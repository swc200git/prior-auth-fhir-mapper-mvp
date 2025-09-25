using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PriorAuthRecord> PriorAuthRecords => Set<PriorAuthRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PriorAuthRecord>().Property(p => p.Id).ValueGeneratedNever();
        modelBuilder.Entity<PriorAuthRecord>().HasIndex(p => p.ReceivedAtUtc);
    }
}