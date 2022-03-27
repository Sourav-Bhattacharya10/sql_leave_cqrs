namespace Application.DTOs.LeaveType;

public class CreateLeaveTypeDto
{
    public string Name { get; set; } = default!;
    public int DefaultDays { get; set; }
}