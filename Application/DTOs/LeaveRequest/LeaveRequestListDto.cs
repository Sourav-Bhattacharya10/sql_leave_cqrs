using System;

using Application.DTOs.Common;
using Application.DTOs.LeaveType;

namespace Application.DTOs.LeaveRequest;

public class LeaveRequestListDto : BaseDto
{
    public LeaveTypeDto LeaveType { get; set; } = default!;
    public DateTime DateRequested { get; set; }
    public bool? Approved { get; set; }
}