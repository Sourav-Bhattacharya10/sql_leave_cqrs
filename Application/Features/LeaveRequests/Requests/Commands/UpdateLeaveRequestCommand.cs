using MediatR;

using Application.DTOs.LeaveRequest;
using Application.Responses;

namespace Application.Features.LeaveRequests.Requests.Commands;

public class UpdateLeaveRequestCommand : IRequest<ResultResponse<LeaveRequestDto>>
{
    public int Id { get; set; }
    public UpdateLeaveRequestDto LeaveRequestDto { get; set; } = default!;

    public ChangeLeaveRequestApprovalDto ChangeLeaveRequestApprovalDto { get; set; } = default!;
}