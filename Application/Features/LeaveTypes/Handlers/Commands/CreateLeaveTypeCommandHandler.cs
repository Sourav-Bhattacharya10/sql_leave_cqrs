using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using Domain;
using Application.DTOs.LeaveType;
using Application.DTOs.LeaveType.Validators;
using Application.Features.LeaveTypes.Requests.Commands;
using Application.Exceptions;
using Application.Responses;
using Persistence.Contracts;
using Application.Enums;

namespace Application.Features.LeaveTypes.Handlers.Commands;

public class CreateLeaveTypeCommandHandler: IRequestHandler<CreateLeaveTypeCommand, ResultResponse<LeaveTypeDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultResponse<LeaveTypeDto>> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveTypeDto>();

        try
        {
            var validator = new CreateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult);
                
            var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);

            leaveType = await _unitOfWork.LeaveTypeRepository.AddAsync(leaveType);

            await _unitOfWork.SaveChangesAsync();

            var leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);

            result = ResultResponse<LeaveTypeDto>.Success(leaveTypeDto, $"Creation of {nameof(LeaveType)} successful");
        }
        catch(ValidationException ex)
        {
            result = ResultResponse<LeaveTypeDto>.Failure(ex.Errors, $"Validation of {nameof(LeaveType)} creation failed", ErrorType.Validation);
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveTypeDto>.Failure(new List<string>() {ex.Message}, $"Creation of {nameof(LeaveType)} failed", ErrorType.Database, ex);
        }

        return result;
    }
}