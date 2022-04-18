using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Application.DTOs.LeaveType;
using Application.Features.LeaveTypes.Requests.Commands;
using Application.Features.LeaveTypes.Requests.Queries;

namespace API.Controllers;

public class LeaveTypesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetLeaveTypeList()
    {
        return HandleResult<List<LeaveTypeDto>>(await Mediator.Send(new GetLeaveTypeListRequest()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLeaveTypeDetail(int id)
    {
        return HandleResult<LeaveTypeDto>(await Mediator.Send(new GetLeaveTypeDetailRequest{ Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateLeaveType(CreateLeaveTypeDto createLeaveTypeDto)
    {
        return HandleResult<LeaveTypeDto>(await Mediator.Send(new CreateLeaveTypeCommand{ LeaveTypeDto = createLeaveTypeDto }));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateLeaveType(LeaveTypeDto dto)
    {
        return HandleResult<LeaveTypeDto>(await Mediator.Send(new UpdateLeaveTypeCommand{ LeaveTypeDto = dto }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLeaveType(int id)
    {
        return HandleResult<LeaveTypeDto>(await Mediator.Send(new DeleteLeaveTypeCommand{ Id = id }));
    }
}