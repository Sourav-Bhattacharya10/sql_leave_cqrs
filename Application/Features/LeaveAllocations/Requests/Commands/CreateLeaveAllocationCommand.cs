using System;
using MediatR;

using Application.DTOs.LeaveAllocation;
using Application.Responses;

namespace Application.Features.LeaveAllocations.Requests.Commands;

public class CreateLeaveAllocationCommand: IRequest<ResultResponse<LeaveAllocationDto>>
{
    public CreateLeaveAllocationDto LeaveAllocationDto { get; set; } = default!;
}