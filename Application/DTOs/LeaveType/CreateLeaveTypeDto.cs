namespace Application.DTOs.LeaveType;

public class CreateLeaveTypeDto : ILeaveTypeDto
{
    public string Name { get; set; } = default!;
    public int DefaultDays { get; set; }
}