using System;
using System.Collections.Generic;
using MediatR;

using Application.DTOs.LeaveType;

namespace Application.Features.LeaveTypes.Requests.Queries;

public class GetLeaveTypeListRequest : IRequest<List<LeaveTypeDto>>
{

}