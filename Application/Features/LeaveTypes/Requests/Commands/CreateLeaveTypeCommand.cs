using System;
using MediatR;

using Application.DTOs.LeaveType;
using Application.Responses;

namespace Application.Features.LeaveTypes.Requests.Commands;

public class CreateLeaveTypeCommand: IRequest<ResultResponse<LeaveTypeDto>>
{
    public CreateLeaveTypeDto LeaveTypeDto { get; set; } = default!;
}