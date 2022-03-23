using System;
using MediatR;

using Application.DTOs.LeaveAllocation;

namespace Application.Features.LeaveAllocations.Requests.Queries;

public class GetLeaveAllocationDetailRequest : IRequest<LeaveAllocationDto>
{
    public int Id { get; set; }
}