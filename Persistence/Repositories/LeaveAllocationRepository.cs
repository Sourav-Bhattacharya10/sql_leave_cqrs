using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Domain;
using Persistence;
using Persistence.Contracts;

namespace Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    private readonly LeaveManagementDbContext _dbContext;

    public LeaveAllocationRepository(LeaveManagementDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LeaveAllocation?> GetLeaveAllocationWithDetailsAsync(int id)
    {
        var leaveAllocation = await _dbContext.LeaveAllocations
                                            .Include(q => q.LeaveType)
                                            .FirstOrDefaultAsync(q => q.Id == id);

        return leaveAllocation;
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync()
    {
        var leaveAllocations = await _dbContext.LeaveAllocations
                                            .Include(q => q.LeaveType)
                                            .ToListAsync();

        return leaveAllocations;
    }
}