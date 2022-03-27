using MediatR;

using Application.DTOs.LeaveType;

namespace Application.Features.LeaveTypes.Requests.Commands;

public class UpdateLeaveTypeCommand : IRequest<Unit>
{
    public LeaveTypeDto LeaveTypeDto { get; set; } = default!;
}