using System;

namespace Application.DTOs.LeaveAllocation;

public class CreateLeaveAllocationDto : ILeaveAllocationDto
{
    public int NumberOfDays { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }   
    public string CreatedBy { get; set; } = default!;
     public string LastModifiedBy { get; set; } = default!; 
}