using System;
using MediatR;

using Application.DTOs.LeaveType;

namespace Application.Features.LeaveTypes.Requests.Commands;

public class CreateLeaveTypeCommand: IRequest<int>
{
    public CreateLeaveTypeDto LeaveTypeDto { get; set; } = default!;
}