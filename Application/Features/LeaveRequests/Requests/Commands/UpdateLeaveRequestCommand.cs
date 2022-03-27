using MediatR;

using Application.DTOs.LeaveRequest;

namespace Application.Features.LeaveRequests.Requests.Commands;

public class UpdateLeaveRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public UpdateLeaveRequestDto LeaveRequestDto { get; set; } = default!;

    public ChangeLeaveRequestApprovalDto ChangeLeaveRequestApprovalDto { get; set; } = default!;
}