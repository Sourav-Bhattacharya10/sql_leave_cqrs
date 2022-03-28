using System;

using Application.DTOs.Common;
using Application.DTOs.LeaveType;

namespace Application.DTOs.LeaveRequest;

public interface ILeaveRequestDto
{
    DateTime StartDate { get; set; }
    DateTime EndDate { get; set; }
    int LeaveTypeId { get; set; }
}