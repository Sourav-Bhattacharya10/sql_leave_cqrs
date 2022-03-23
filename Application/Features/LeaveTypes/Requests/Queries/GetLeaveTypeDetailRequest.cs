using System;
using MediatR;

using Application.DTOs.LeaveType;

namespace Application.Features.LeaveTypes.Requests.Queries;

public class GetLeaveTypeDetailRequest : IRequest<LeaveTypeDto>
{
    public int Id { get; set; }
}