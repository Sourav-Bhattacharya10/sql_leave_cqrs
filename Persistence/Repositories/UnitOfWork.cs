using System;
using System.Threading.Tasks;
using Persistence;
using Persistence.Contracts;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LeaveManagementDbContext _context;

    private ILeaveTypeRepository _leaveTypeRepository = default!;
    private ILeaveRequestRepository _leaveRequestRepository = default!;
    private ILeaveAllocationRepository _leaveAllocationRepository = default!;

    public ILeaveTypeRepository LeaveTypeRepository => _leaveTypeRepository ??= new LeaveTypeRepository(_context);
    public ILeaveRequestRepository LeaveRequestRepository => _leaveRequestRepository ??= new LeaveRequestRepository(_context);
    public ILeaveAllocationRepository LeaveAllocationRepository => _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);

    public UnitOfWork(LeaveManagementDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}