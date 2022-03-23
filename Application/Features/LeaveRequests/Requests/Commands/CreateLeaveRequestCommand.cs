using System;
using MediatR;

using Application.DTOs.LeaveRequest;

namespace Application.Features.LeaveRequests.Requests.Commands;

public class CreateLeaveRequestCommand : IRequest<int>
{
    public CreateLeaveRequestDto LeaveRequestDto { get; set; } = default!;
}