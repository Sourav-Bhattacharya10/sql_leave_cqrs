using System.Threading.Tasks;

using Domain;

namespace Persistence.Contracts;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);
    Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetailsAsync();
}