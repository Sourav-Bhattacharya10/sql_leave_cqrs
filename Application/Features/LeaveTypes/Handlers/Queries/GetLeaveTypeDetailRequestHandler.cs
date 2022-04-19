using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using Domain;
using Application.DTOs.LeaveType;
using Application.Responses;
using Application.Exceptions;
using Application.Features.LeaveTypes.Requests.Queries;
using Persistence.Contracts;
using Application.Enums;


namespace Application.Features.LeaveTypes.Handlers.Queries;

public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, ResultResponse<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public GetLeaveTypeDetailRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<ResultResponse<LeaveTypeDto>> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveTypeDto>();

        try
        {
            var leaveType = await _leaveTypeRepository.GetAsync(request.Id);

            if(leaveType == null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            var leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);

            result = ResultResponse<LeaveTypeDto>.Success(leaveTypeDto, $"Fetch of {nameof(LeaveType)} object successful");
        }
        catch (NotFoundException ex)
        {
            result = ResultResponse<LeaveTypeDto>.Failure(new List<string>() {ex.Message}, $"Fetch of {nameof(LeaveType)} object failed as the record was not found", ErrorType.NotFound);
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveTypeDto>.Failure(new List<string>() {ex.Message}, $"Fetch of {nameof(LeaveType)} object failed", ErrorType.Database, ex);
        }

        return result;
    }
}