using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using Application.DTOs.LeaveType;
using Application.Features.LeaveTypes.Requests.Queries;
using Persistence.Contracts;


namespace Application.Features.LeaveTypes.Handlers.Queries;

public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, LeaveTypeDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public GetLeaveTypeDetailRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<LeaveTypeDto> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetAsync(request.Id);
        return _mapper.Map<LeaveTypeDto>(leaveType);
    }
}