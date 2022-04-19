namespace Application.DTOs.LeaveType;

public class CreateLeaveTypeDto : ILeaveTypeDto
{
    public string Name { get; set; } = default!;
    public int DefaultDays { get; set; }
    public string CreatedBy { get; set; } = default!;
     public string LastModifiedBy { get; set; } = default!;
}