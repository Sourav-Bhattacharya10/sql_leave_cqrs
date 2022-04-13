using MediatR;

using Application.DTOs.LeaveType;
using Application.Responses;

namespace Application.Features.LeaveTypes.Requests.Commands;

public class UpdateLeaveTypeCommand : IRequest<ResultResponse<LeaveTypeDto>>
{
    public LeaveTypeDto LeaveTypeDto { get; set; } = default!;
}