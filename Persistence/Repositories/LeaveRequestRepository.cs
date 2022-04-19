using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Domain;
using Persistence;
using Persistence.Contracts;

namespace Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    private readonly LeaveManagementDbContext _dbContext;

    public LeaveRequestRepository(LeaveManagementDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LeaveRequest?> GetLeaveRequestWithDetailsAsync(int id)
    {
        var leaveRequest = await _dbContext.LeaveRequests
                                            .Include(q => q.LeaveType)
                                            .FirstOrDefaultAsync(q => q.Id == id);

        return leaveRequest;
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetailsAsync()
    {
        var leaveRequests = await _dbContext.LeaveRequests
                                            .Include(q => q.LeaveType)
                                            .ToListAsync();

        return leaveRequests;
    }

    public LeaveRequest ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus)
    {
        leaveRequest.Approved = ApprovalStatus;
        _dbContext.Entry(leaveRequest).State = EntityState.Modified;

        return leaveRequest;
    }
}