using System;
using MediatR;

using Application.DTOs.LeaveRequest;

namespace Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestDetailRequest : IRequest<LeaveRequestDto>
{
    public int Id { get; set; }
}