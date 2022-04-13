using MediatR;

using Application.Responses;
using Application.DTOs.LeaveType;

namespace Application.Features.LeaveTypes.Requests.Commands;

public class DeleteLeaveTypeCommand : IRequest<ResultResponse<LeaveTypeDto>>
{
    public int Id { get; set; }
}