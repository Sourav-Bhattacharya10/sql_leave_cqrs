using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using Domain;
using Application.DTOs.LeaveAllocation;
using Application.Features.LeaveAllocations.Requests.Commands;
using Application.Exceptions;
using Application.Responses;
using Persistence.Contracts;
using Application.Enums;

namespace Application.Features.LeaveAllocations.Handlers.Commands;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, ResultResponse<LeaveAllocationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultResponse<LeaveAllocationDto>> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveAllocationDto>();

        try
        {
            var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.GetAsync(request.Id);

            if(leaveAllocation == null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            leaveAllocation = _unitOfWork.LeaveAllocationRepository.Delete(leaveAllocation);

            await _unitOfWork.SaveChangesAsync();

            var leaveAllocationDto = _mapper.Map<LeaveAllocationDto>(leaveAllocation);

            result = ResultResponse<LeaveAllocationDto>.Success(leaveAllocationDto, $"Deletion of {nameof(LeaveAllocation)} successful");
        }
        catch (NotFoundException ex)
        {
            result = ResultResponse<LeaveAllocationDto>.Failure(new List<string>() {ex.Message}, $"Deletion of {nameof(LeaveAllocation)} failed as the record was not found", ErrorType.NotFound);
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveAllocationDto>.Failure(new List<string>() {ex.Message}, $"Deletion of {nameof(LeaveAllocation)} failed", ErrorType.Database, ex);
        }

        return result;
    }
}