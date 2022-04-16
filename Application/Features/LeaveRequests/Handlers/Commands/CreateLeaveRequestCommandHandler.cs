using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using Domain;
using Application.DTOs.LeaveRequest;
using Application.DTOs.LeaveRequest.Validators;
using Application.Features.LeaveRequests.Requests.Commands;
using Application.Exceptions;
using Application.Responses;
using Persistence.Contracts;
using Infrastructure.Models;
using Infrastructure.Contracts.EmailContract;

namespace Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, ResultResponse<LeaveRequestDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;

    public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
    }

    public async Task<ResultResponse<LeaveRequestDto>> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveRequestDto>();

        try
        {
            var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);

            leaveRequest = await _leaveRequestRepository.AddAsync(leaveRequest);

            var leaveRequestDto = _mapper.Map<LeaveRequestDto>(leaveRequest);

            result = ResultResponse<LeaveRequestDto>.Success(leaveRequestDto, $"Creation of {nameof(LeaveRequest)} is successful");

            var email = new Email {
                To = "sourav.bhattacharya3@gmail.com",
                Subject = "Leave Request Created",
                Body = $"Your leave request for {leaveRequestDto.StartDate} to {leaveRequestDto.EndDate} has been created successfully."
            };

            await _emailSender.SendEmailAsync(email);
        }
        catch (ValidationException ex)
        {
            result = ResultResponse<LeaveRequestDto>.Failure(ex.Errors, $"Creation of {nameof(LeaveRequest)} is failed");
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveRequestDto>.Failure(new List<string>() {ex.Message}, $"Creation of {nameof(LeaveRequest)} is failed");
        }

        return result;
    }
}