using System;
using System.Collections.Generic;

using Application.Enums;

namespace Application.Responses;

public class ResultResponse<T>
{
    public bool IsSuccess { get; set; }
    public T Value {get; set;} = default!;
    public string Message { get; set; } = default!;
    public ErrorType ErrorType { get; set; }
    public List<string> Errors { get; set; } = default!;

    public Exception ExceptionObject { get; set; } = default!;

    public static ResultResponse<T> Success(T value, string message) => new ResultResponse<T> {IsSuccess = true, Value = value, Message = message, ErrorType = ErrorType.None};
    public static ResultResponse<T> Failure(List<string> errors, string message, ErrorType errorType, Exception exceptionObject = default!) => new ResultResponse<T> {IsSuccess = false, Errors = errors, Message = message, ErrorType = errorType, ExceptionObject = exceptionObject};
}