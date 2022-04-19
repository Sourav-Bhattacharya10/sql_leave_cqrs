using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using AutoMapper;

using Domain;
using Application.DTOs.LeaveType;
using Application.Responses;
using Application.Features.LeaveTypes.Requests.Queries;
using Persistence.Contracts;
using Application.Enums;


namespace Application.Features.LeaveTypes.Handlers.Queries;

public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, ResultResponse<List<LeaveTypeDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLeaveTypeListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultResponse<List<LeaveTypeDto>>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<List<LeaveTypeDto>>();

        try
        {
            var leaveTypes = await _unitOfWork.LeaveTypeRepository.GetAllAsync();
            var leaveTypesDto = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            result = ResultResponse<List<LeaveTypeDto>>.Success(leaveTypesDto, $"Fetch of {nameof(LeaveType)} list successful");
        }
        catch (Exception ex)
        {
            result = ResultResponse<List<LeaveTypeDto>>.Failure(new List<string>() {ex.Message}, $"Fetch of {nameof(LeaveType)} list failed", ErrorType.Database, ex);
        }

        return result;
    }
}