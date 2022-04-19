using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using Domain;
using Application.DTOs.LeaveRequest;
using Application.Responses;
using Application.Exceptions;
using Application.Features.LeaveRequests.Requests.Queries;
using Persistence.Contracts;
using Application.Enums;


namespace Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, ResultResponse<LeaveRequestDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }

    public async Task<ResultResponse<LeaveRequestDto>> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveRequestDto>();

        try
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetailsAsync(request.Id);

            if(leaveRequest == null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);

            var leaveRequestDto = _mapper.Map<LeaveRequestDto>(leaveRequest);

            result = ResultResponse<LeaveRequestDto>.Success(leaveRequestDto, $"Fetch of {nameof(LeaveRequest)} object successful");
        }
        catch (NotFoundException ex)
        {
            result = ResultResponse<LeaveRequestDto>.Failure(new List<string>() {ex.Message}, $"Fetch of {nameof(LeaveRequest)} object failed as the record was not found", ErrorType.NotFound);
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveRequestDto>.Failure(new List<string>() {ex.Message}, $"Fetch of {nameof(LeaveRequest)} object failed", ErrorType.Database, ex);
        }

        return result;
    }
}