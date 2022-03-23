using Application.DTOs.Common;

namespace Application.DTOs.LeaveType;

public class LeaveTypeDto : BaseDto
{
    public string Name { get; set; } = default!;
    public int DefaultDays { get; set; }
}