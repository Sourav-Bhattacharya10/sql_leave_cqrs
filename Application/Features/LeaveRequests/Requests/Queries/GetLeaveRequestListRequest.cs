using System;
using System.Collections.Generic;
using MediatR;

using Application.DTOs.LeaveRequest;
using Application.Responses;

namespace Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestListRequest : IRequest<ResultResponse<List<LeaveRequestListDto>>>
{

}