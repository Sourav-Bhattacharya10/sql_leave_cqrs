using System;
using System.Collections.Generic;
using MediatR;

using Application.DTOs.LeaveType;
using Application.Responses;

namespace Application.Features.LeaveTypes.Requests.Queries;

public class GetLeaveTypeListRequest : IRequest<ResultResponse<List<LeaveTypeDto>>>
{

}