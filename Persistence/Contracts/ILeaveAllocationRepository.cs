using System.Threading.Tasks;

using Domain;

namespace Persistence.Contracts;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id);
    Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync();
}