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
    }
}