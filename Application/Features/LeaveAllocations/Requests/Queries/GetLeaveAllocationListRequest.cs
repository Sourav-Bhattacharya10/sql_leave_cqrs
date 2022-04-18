using System;
using System.Collections.Generic;
using MediatR;

using Application.DTOs.LeaveAllocation;
using Application.Responses;

namespace Application.Features.LeaveAllocations.Requests.Queries;

public class GetLeaveAllocationListRequest : IRequest<ResultResponse<List<LeaveAllocationDto>>>
{

}