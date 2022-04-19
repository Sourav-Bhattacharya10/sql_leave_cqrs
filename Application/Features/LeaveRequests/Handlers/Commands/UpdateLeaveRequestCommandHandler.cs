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
using Application.Enums;

namespace Application.Features.LeaveRequests.Handlers.Commands;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, ResultResponse<LeaveRequestDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResultResponse<LeaveRequestDto>> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var result = new ResultResponse<LeaveRequestDto>();

        try
        {
            var validator = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var leaveRequest = await _unitOfWork.LeaveRequestRepository.GetAsync(request.Id);

            if(leaveRequest == null)
                throw new NotFoundException(nameof(LeaveRequest), request.LeaveRequestDto.Id);

            if(request.LeaveRequestDto != null)
            {
                _mapper.Map(request.LeaveRequestDto, leaveRequest);

                leaveRequest = _unitOfWork.LeaveRequestRepository.Update(leaveRequest);
                
                var leaveRequestDto = _mapper.Map<LeaveRequestDto>(leaveRequest);

                result = ResultResponse<LeaveRequestDto>.Success(leaveRequestDto, $"Updation of {nameof(LeaveRequest)} successful");
            }
            else if(request.ChangeLeaveRequestApprovalDto != null)
            {
                leaveRequest = _unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);

                var leaveRequestDto = _mapper.Map<LeaveRequestDto>(leaveRequest);

                result = ResultResponse<LeaveRequestDto>.Success(leaveRequestDto, $"Approval of {nameof(LeaveRequest)} successful");
            }

            await _unitOfWork.SaveChangesAsync();
        }
        catch (ValidationException ex)
        {
            result = ResultResponse<LeaveRequestDto>.Failure(ex.Errors, $"Validation of {nameof(LeaveRequest)} updation failed", ErrorType.Validation);
        }
        catch (NotFoundException ex)
        {
            result = ResultResponse<LeaveRequestDto>.Failure(new List<string>(){ ex.Message }, $"Updation of {nameof(LeaveRequest)} failed as the record was not found", ErrorType.NotFound);
        }
        catch (Exception ex)
        {
            result = ResultResponse<LeaveRequestDto>.Failure(new List<string>(){ ex.Message }, $"Updation of {nameof(LeaveRequest)} failed", ErrorType.Database, ex);
        }

        return result;
    }
}