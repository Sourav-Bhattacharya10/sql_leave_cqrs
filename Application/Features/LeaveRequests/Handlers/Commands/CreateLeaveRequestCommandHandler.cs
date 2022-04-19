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
// using Infrastructure.Models;
// using Infrastructure.Contracts.EmailContract;
using Application.Enums;

namespace Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, ResultResponse<LeaveRequestDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    // private readonly IEmailSender _emailSender;

    public CreateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) // , IEmailSender emailSender
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        // _emailSender = emailSender;
    }

    public async Task<ResultResponse<LeaveRequestDto>> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveRequestDto>();

        try
        {
            var validator = new CreateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);

            leaveRequest = await _unitOfWork.LeaveRequestRepository.AddAsync(leaveRequest);

            await _unitOfWork.SaveChangesAsync();

            var leaveRequestDto = _mapper.Map<LeaveRequestDto>(leaveRequest);

            result = ResultResponse<LeaveRequestDto>.Success(leaveRequestDto, $"Creation of {nameof(LeaveRequest)} successful");

            // var email = new Email {
            //     To = "sourav.bhattacharya3@gmail.com",
            //     Subject = "Leave Request Created",
            //     Body = $"Your leave request for {leaveRequestDto.StartDate} to {leaveRequestDto.EndDate} has been created successfully."
            // };

            // await _emailSender.SendEmailAsync(email);
        }
        catch (ValidationException ex)
        {
            result = ResultResponse<LeaveRequestDto>.Failure(ex.Errors, $"Validation of {nameof(LeaveRequest)} creation failed", ErrorType.Validation);
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveRequestDto>.Failure(new List<string>() {ex.Message}, $"Creation of {nameof(LeaveRequest)} failed", ErrorType.Database, ex);
        }

        return result;
    }
}