using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using Domain;
using Application.DTOs.LeaveType;
using Application.Features.LeaveTypes.Requests.Commands;
using Application.Exceptions;
using Application.Responses;
using Persistence.Contracts;
using Application.Enums;

namespace Application.Features.LeaveTypes.Handlers.Commands;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, ResultResponse<LeaveTypeDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultResponse<LeaveTypeDto>> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveTypeDto>();

        try
        {
            var leaveType = await _unitOfWork.LeaveTypeRepository.GetAsync(request.Id);

            if(leaveType == null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            leaveType = _unitOfWork.LeaveTypeRepository.Delete(leaveType);

            await _unitOfWork.SaveChangesAsync();

            var leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);

            result = ResultResponse<LeaveTypeDto>.Success(leaveTypeDto, $"Deletion of {nameof(LeaveType)} successful");
        }
        catch (NotFoundException ex)
        {
            result = ResultResponse<LeaveTypeDto>.Failure(new List<string>(){ ex.Message }, $"Deletion of {nameof(LeaveType)} failed as the record was not found", ErrorType.NotFound);
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveTypeDto>.Failure(new List<string>(){ ex.Message }, $"Deletion of {nameof(LeaveType)} failed", ErrorType.Database, ex);
        }

        return result;
    }
}