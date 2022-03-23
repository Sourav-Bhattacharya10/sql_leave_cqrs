using System;
using MediatR;

using Application.DTOs.LeaveType;

namespace Application.Features.LeaveTypes.Requests.Commands;

public class CreateLeaveTypeCommand: IRequest<int>
{
    public LeaveTypeDto LeaveTypeDto { get; set; } = default!;
}