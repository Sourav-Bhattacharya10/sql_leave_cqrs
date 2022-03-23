using System;
using MediatR;

using Application.DTOs.LeaveAllocation;

namespace Application.Features.LeaveAllocations.Requests.Commands;

public class CreateLeaveAllocationCommand: IRequest<int>
{
    public CreateLeaveAllocationDto LeaveAllocationDto { get; set; } = default!;
}