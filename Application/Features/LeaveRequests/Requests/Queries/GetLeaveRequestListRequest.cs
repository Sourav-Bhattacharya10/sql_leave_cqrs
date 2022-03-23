using System;
using System.Collections.Generic;
using MediatR;

using Application.DTOs.LeaveRequest;

namespace Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestListRequest : IRequest<List<LeaveRequestListDto>>
{

}