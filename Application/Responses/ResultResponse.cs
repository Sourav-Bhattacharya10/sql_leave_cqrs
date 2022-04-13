using System.Collections.Generic;

namespace Application.Responses;

public class ResultResponse<T>
{
    public bool IsSuccess { get; set; }
    public T Value {get; set;} = default!;
    public string Message { get; set; } = default!;
    public List<string> Errors { get; set; } = default!;

    public static ResultResponse<T> Success(T value, string message) => new ResultResponse<T> {IsSuccess = true, Value = value, Message = message};
    public static ResultResponse<T> Failure(List<string> errors, string message) => new ResultResponse<T> {IsSuccess = false, Errors = errors, Message = message};
}