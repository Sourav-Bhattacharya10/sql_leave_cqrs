using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using Domain;
using Application.DTOs.LeaveType.Validators;
using Application.Features.LeaveTypes.Requests.Commands;
using Persistence.Contracts;

namespace Application.Features.LeaveTypes.Handlers.Commands;

public class CreateLeaveTypeCommandHandler: IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveTypeDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

        if(!validationResult.IsValid)
            throw new Exception();
            
        var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);

        leaveType = await _leaveTypeRepository.AddAsync(leaveType);

        return leaveType.Id;
    }
}