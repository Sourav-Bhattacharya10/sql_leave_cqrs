using System;
using MediatR;

using Application.DTOs.LeaveRequest;
using Application.Responses;

namespace Application.Features.LeaveRequests.Requests.Commands;

public class CreateLeaveRequestCommand : IRequest<ResultResponse<LeaveRequestDto>>
{
    public CreateLeaveRequestDto LeaveRequestDto { get; set; } = default!;
}