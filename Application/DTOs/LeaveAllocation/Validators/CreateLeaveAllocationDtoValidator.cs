using FluentValidation;

using Application.DTOs.LeaveAllocation;
using Persistence.Contracts;

namespace Application.DTOs.LeaveAllocation.Validators;

public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        
        Include(new ILeaveAllocationDtoValidator(_leaveTypeRepository));

        RuleFor(p => p.CreatedBy)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters");

        RuleFor(p => p.LastModifiedBy)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters");
    }
}