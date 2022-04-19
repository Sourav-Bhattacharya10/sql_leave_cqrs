using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using Domain;
using Application.DTOs.LeaveAllocation;
using Application.DTOs.LeaveAllocation.Validators;
using Application.Features.LeaveAllocations.Requests.Commands;
using Application.Exceptions;
using Application.Responses;
using Persistence.Contracts;
using Application.Enums;

namespace Application.Features.LeaveAllocations.Handlers.Commands;

public class CreateLeaveAllocationCommandHandler: IRequestHandler<CreateLeaveAllocationCommand, ResultResponse<LeaveAllocationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultResponse<LeaveAllocationDto>> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveAllocationDto>();

        try
        {
            var validator = new CreateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var leaveAllocation = _mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);

            leaveAllocation = await _unitOfWork.LeaveAllocationRepository.AddAsync(leaveAllocation);

            await _unitOfWork.SaveChangesAsync();

            var leaveAllocationDto = _mapper.Map<LeaveAllocationDto>(leaveAllocation);

            result = ResultResponse<LeaveAllocationDto>.Success(leaveAllocationDto, $"Creation of {nameof(LeaveAllocation)} successful");
        }
        catch (ValidationException ex)
        {
            result = ResultResponse<LeaveAllocationDto>.Failure(ex.Errors, $"Validation of {nameof(LeaveAllocation)} creation failed", ErrorType.Validation);
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveAllocationDto>.Failure(new List<string>() {ex.Message}, $"Creation of {nameof(LeaveAllocation)} failed", ErrorType.Database, ex);
        }

        return result;
    }
}