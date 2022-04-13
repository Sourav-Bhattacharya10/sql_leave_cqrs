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

namespace Application.Features.LeaveAllocations.Handlers.Commands;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, ResultResponse<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<ResultResponse<LeaveAllocationDto>> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveAllocationDto>();

        try
        {
            var validator = new UpdateLeaveAllocationDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var leaveAllocation = await _leaveAllocationRepository.GetAsync(request.LeaveAllocationDto.Id);

            if(leaveAllocation == null)
                throw new NotFoundException(nameof(LeaveAllocation), request.LeaveAllocationDto.Id);

            _mapper.Map(request.LeaveAllocationDto, leaveAllocation);

            leaveAllocation = await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

            var leaveAllocationDto = _mapper.Map<LeaveAllocationDto>(leaveAllocation);

            result = ResultResponse<LeaveAllocationDto>.Success(leaveAllocationDto, $"Updation of {nameof(LeaveAllocation)} is successful");
        }
        catch (ValidationException ex)
        {
            result = ResultResponse<LeaveAllocationDto>.Failure(ex.Errors, $"Updation of {nameof(LeaveAllocation)} is failed");
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveAllocationDto>.Failure(new List<string>() {ex.Message}, $"Updation of {nameof(LeaveAllocation)} is failed");
        }

        return result;
    }
}