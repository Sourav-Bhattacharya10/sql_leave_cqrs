using System;

using Application.DTOs.Common;

namespace Application.DTOs.LeaveRequest;

public class CreateLeaveRequestDto : BaseDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LeaveTypeId { get; set; }
    public string RequestComments { get; set; } = default!;
}