using FluentValidation;

using Application.DTOs.LeaveType;

namespace Application.DTOs.LeaveType.Validators;

public class CreateLeaveTypeDtoValidator : AbstractValidator<CreateLeaveTypeDto>
{
    public CreateLeaveTypeDtoValidator()
    {
        Include(new ILeaveTypeDtoValidator());
    }
}