using System;
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

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, ResultResponse<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<ResultResponse<LeaveTypeDto>> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveTypeDto>();

        try
        {
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var leaveType = await _leaveTypeRepository.GetAsync(request.LeaveTypeDto.Id);

            if(leaveType == null)
                throw new NotFoundException(nameof(LeaveType), request.LeaveTypeDto.Id);

            _mapper.Map(request.LeaveTypeDto, leaveType);

            leaveType =  await _leaveTypeRepository.UpdateAsync(leaveType);

            var leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);

            result = ResultResponse<LeaveTypeDto>.Success(leaveTypeDto, $"Updation of {nameof(LeaveType)} successful");
        }
        catch (ValidationException ex)
        {
            result = ResultResponse<LeaveTypeDto>.Failure(ex.Errors, $"Validation of {nameof(LeaveType)} updation failed", ErrorType.Validation);
        }
        catch (NotFoundException ex)
        {
            result = ResultResponse<LeaveTypeDto>.Failure(new List<string>() {ex.Message}, $"Updation of {nameof(LeaveType)} failed as the record was not found", ErrorType.NotFound);
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveTypeDto>.Failure(new List<string>() {ex.Message}, $"Updation of {nameof(LeaveType)} is failed", ErrorType.Database, ex);
        }

        return result;
    }
}