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

namespace Application.Features.LeaveTypes.Handlers.Commands;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, ResultResponse<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<ResultResponse<LeaveTypeDto>> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveTypeDto>();

        try
        {
            var leaveType = await _leaveTypeRepository.GetAsync(request.Id);

            if(leaveType == null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            leaveType = await _leaveTypeRepository.DeleteAsync(leaveType);

            var leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);

            result = ResultResponse<LeaveTypeDto>.Success(leaveTypeDto, $"Deletion of {nameof(LeaveType)} is successful");
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveTypeDto>.Failure(new List<string>(){ ex.Message }, $"Deletion of {nameof(LeaveType)} is failed");
        }

        return result;
    }
}