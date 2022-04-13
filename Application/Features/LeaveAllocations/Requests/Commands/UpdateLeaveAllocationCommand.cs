using MediatR;

using Application.DTOs.LeaveAllocation;
using Application.Responses;

namespace Application.Features.LeaveAllocations.Requests.Commands;

public class UpdateLeaveAllocationCommand : IRequest<ResultResponse<LeaveAllocationDto>>
{
    public UpdateLeaveAllocationDto LeaveAllocationDto { get; set; } = default!;
}