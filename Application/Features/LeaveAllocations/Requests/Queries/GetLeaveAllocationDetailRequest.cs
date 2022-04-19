using System;
using MediatR;

using Application.DTOs.LeaveAllocation;
using Application.Responses;

namespace Application.Features.LeaveAllocations.Requests.Queries;

public class GetLeaveAllocationDetailRequest : IRequest<ResultResponse<LeaveAllocationDto>>
{
    public int Id { get; set; }
}