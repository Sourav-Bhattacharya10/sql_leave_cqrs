using System;

using Domain.Common;

namespace Domain;

public class LeaveType : BaseDomainEntity
{
    public string Name { get; set; } = default!;
    public int DefaultDays { get; set; }
}