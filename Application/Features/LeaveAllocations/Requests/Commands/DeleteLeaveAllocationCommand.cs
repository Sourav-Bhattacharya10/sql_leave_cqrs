using MediatR;

using Application.DTOs.LeaveAllocation;
using Application.Responses;

namespace Application.Features.LeaveAllocations.Requests.Commands;

public class DeleteLeaveAllocationCommand : IRequest<ResultResponse<LeaveAllocationDto>>
{
    public int Id { get; set; }
}