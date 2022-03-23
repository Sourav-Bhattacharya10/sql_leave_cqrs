using System;
using System.Collections.Generic;
using MediatR;

using Application.DTOs.LeaveAllocation;

namespace Application.Features.LeaveAllocations.Requests.Queries;

public class GetLeaveAllocationListRequest : IRequest<List<LeaveAllocationDto>>
{

}