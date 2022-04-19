using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using AutoMapper;

using Domain;
using Application.DTOs.LeaveAllocation;
using Application.Responses;
using Application.Features.LeaveAllocations.Requests.Queries;
using Persistence.Contracts;
using Application.Enums;


namespace Application.Features.LeaveAllocations.Handlers.Queries;

public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, ResultResponse<List<LeaveAllocationDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLeaveAllocationListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultResponse<List<LeaveAllocationDto>>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<List<LeaveAllocationDto>>();

        try
        {
            var leaveAllocations = await _unitOfWork.LeaveAllocationRepository.GetLeaveAllocationsWithDetailsAsync();
            var leaveAllocationsDto = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

            result = ResultResponse<List<LeaveAllocationDto>>.Success(leaveAllocationsDto, $"Fetch of {nameof(LeaveAllocation)} list successful");
        }
        catch (Exception ex)
        {
            result = ResultResponse<List<LeaveAllocationDto>>.Failure(new List<string>() {ex.Message}, $"Fetch of {nameof(LeaveAllocation)} list failed", ErrorType.Database, ex);
        }

        return result;
    }
}