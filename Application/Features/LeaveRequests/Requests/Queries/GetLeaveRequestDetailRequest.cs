using System;
using MediatR;

using Application.DTOs.LeaveRequest;
using Application.Responses;

namespace Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestDetailRequest : IRequest<ResultResponse<LeaveRequestDto>>
{
    public int Id { get; set; }
}