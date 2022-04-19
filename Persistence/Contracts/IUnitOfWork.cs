using System;
using System.Threading.Tasks;

namespace Persistence.Contracts;

public interface IUnitOfWork : IDisposable
{
    ILeaveTypeRepository LeaveTypeRepository { get; }
    ILeaveRequestRepository LeaveRequestRepository { get; }
    ILeaveAllocationRepository LeaveAllocationRepository { get; }

    Task SaveChangesAsync();
}