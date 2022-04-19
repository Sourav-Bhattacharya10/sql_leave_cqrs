using System.Threading.Tasks;
using System.Collections.Generic;

using Domain;

namespace Persistence.Contracts;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest?> GetLeaveRequestWithDetailsAsync(int id);
    Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetailsAsync();
    LeaveRequest ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus);
}