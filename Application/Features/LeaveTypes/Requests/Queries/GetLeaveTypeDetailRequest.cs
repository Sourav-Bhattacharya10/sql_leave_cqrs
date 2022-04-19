using System;
using MediatR;

using Application.DTOs.LeaveType;
using Application.Responses;

namespace Application.Features.LeaveTypes.Requests.Queries;

public class GetLeaveTypeDetailRequest : IRequest<ResultResponse<LeaveTypeDto>>
{
    public int Id { get; set; }
}