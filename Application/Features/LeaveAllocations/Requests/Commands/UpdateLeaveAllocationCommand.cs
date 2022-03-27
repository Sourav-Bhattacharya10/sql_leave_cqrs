using MediatR;

using Application.DTOs.LeaveAllocation;

namespace Application.Features.LeaveAllocations.Requests.Commands;

public class UpdateLeaveAllocationCommand : IRequest<Unit>
{
    public UpdateLeaveAllocationDto LeaveAllocationDto { get; set; } = default!;
}