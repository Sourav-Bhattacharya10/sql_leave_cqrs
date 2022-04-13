using MediatR;

using Application.DTOs.LeaveRequest;
using Application.Responses;

namespace Application.Features.LeaveRequests.Requests.Commands;

public class DeleteLeaveRequestCommand : IRequest<ResultResponse<LeaveRequestDto>>
{
    public int Id { get; set; }
}