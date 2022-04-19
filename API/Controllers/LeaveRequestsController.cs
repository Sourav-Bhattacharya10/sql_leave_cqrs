using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Application.DTOs.LeaveRequest;
using Application.Features.LeaveRequests.Requests.Commands;
using Application.Features.LeaveRequests.Requests.Queries;

namespace API.Controllers;

public class LeaveRequestsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetLeaveRequestList()
    {
        return HandleResult<List<LeaveRequestListDto>>(await Mediator.Send(new GetLeaveRequestListRequest()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLeaveRequestDetail(int id)
    {
        return HandleResult<LeaveRequestDto>(await Mediator.Send(new GetLeaveRequestDetailRequest{ Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateLeaveRequest(CreateLeaveRequestDto createLeaveRequestDto)
    {
        return HandleResult<LeaveRequestDto>(await Mediator.Send(new CreateLeaveRequestCommand{ LeaveRequestDto = createLeaveRequestDto }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLeaveRequest(int id, [FromBody] UpdateLeaveRequestDto updateLeaveRequestDto)
    {
        return HandleResult<LeaveRequestDto>(await Mediator.Send(new UpdateLeaveRequestCommand{ Id = id, LeaveRequestDto = updateLeaveRequestDto }));
    }

    [HttpPut("{id}/changeapproval")]
    public async Task<IActionResult> ChangeLeaveRequestApproval(int id, [FromBody] ChangeLeaveRequestApprovalDto dto)
    {
        return HandleResult<LeaveRequestDto>(await Mediator.Send(new UpdateLeaveRequestCommand{ Id = id, ChangeLeaveRequestApprovalDto = dto }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLeaveRequest(int id)
    {
        return HandleResult<LeaveRequestDto>(await Mediator.Send(new DeleteLeaveRequestCommand{ Id = id }));
    }
}