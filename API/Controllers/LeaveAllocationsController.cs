using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Application.DTOs.LeaveAllocation;
using Application.Features.LeaveAllocations.Requests.Commands;
using Application.Features.LeaveAllocations.Requests.Queries;

namespace API.Controllers;

public class LeaveAllocationsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetLeaveAllocationList()
    {
        return HandleResult<List<LeaveAllocationDto>>(await Mediator.Send(new GetLeaveAllocationListRequest()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLeaveAllocationDetail(int id)
    {
        return HandleResult<LeaveAllocationDto>(await Mediator.Send(new GetLeaveAllocationDetailRequest{ Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateLeaveAllocation(CreateLeaveAllocationDto createLeaveAllocationDto)
    {
        return HandleResult<LeaveAllocationDto>(await Mediator.Send(new CreateLeaveAllocationCommand{ LeaveAllocationDto = createLeaveAllocationDto }));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateLeaveAllocation(UpdateLeaveAllocationDto updateLeaveAllocationDto)
    {
        return HandleResult<LeaveAllocationDto>(await Mediator.Send(new UpdateLeaveAllocationCommand{ LeaveAllocationDto = updateLeaveAllocationDto }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLeaveAllocation(int id)
    {
        return HandleResult<LeaveAllocationDto>(await Mediator.Send(new DeleteLeaveAllocationCommand{ Id = id }));
    }
}