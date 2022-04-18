using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using AutoMapper;

using Domain;
using Application.DTOs.LeaveRequest;
using Application.Responses;
using Application.Features.LeaveRequests.Requests.Queries;
using Persistence.Contracts;
using Application.Enums;


namespace Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, ResultResponse<List<LeaveRequestListDto>>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }

    public async Task<ResultResponse<List<LeaveRequestListDto>>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<List<LeaveRequestListDto>>();

        try
        {
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsAsync();
            var leaveRequestsDto = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

            result = ResultResponse<List<LeaveRequestListDto>>.Success(leaveRequestsDto, $"Fetch of {nameof(LeaveRequest)} list successful");
        }
        catch (Exception ex)
        {
            result = ResultResponse<List<LeaveRequestListDto>>.Failure(new List<string>() {ex.Message}, $"Fetch of {nameof(LeaveRequest)} list failed", ErrorType.Database, ex);
        }

        return result;
    }
}