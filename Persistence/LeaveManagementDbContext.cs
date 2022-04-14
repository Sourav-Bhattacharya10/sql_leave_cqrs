using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Domain;
using Domain.Common;

namespace Persistence;

public class LeaveManagementDbContext : DbContext
{
    public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveManagementDbContext).Assembly);
    }

    public  override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
        {
            entry.Entity.LastModifiedDate = DateTime.Now;

            if(entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<LeaveType> LeaveTypes { get; set; } = default!;
    public DbSet<LeaveRequest> LeaveRequests { get; set; } = default!;
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; } = default!;
}