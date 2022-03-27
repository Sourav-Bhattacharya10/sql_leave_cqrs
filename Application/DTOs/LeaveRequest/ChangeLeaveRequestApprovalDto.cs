using Application.DTOs.Common;

namespace Application.DTOs.LeaveRequest;

public class ChangeLeaveRequestApprovalDto : BaseDto
{
    public bool? Approved { get; set; }
}