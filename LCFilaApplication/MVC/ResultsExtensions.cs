using LCFilaApplication.Helpers;
using Microsoft.AspNetCore.Http;

namespace LCFilaApplication.MVC;

public static class ResultsExtensions
{
    public static IResult MapResult<T>(this IResultExtensions resultExtensions, Results<T> result)
    {
        ArgumentNullException.ThrowIfNull(resultExtensions);

        return result.Match(
                        onSuccess: value => Results.Ok(value),
                        onFailure: error => GetErrorResult(error));
    }
    
    internal static IResult GetErrorResult(Error error)
    {
        return error.ErrorType switch
        {
            ErrorType.Validation => Results.UnprocessableEntity(error),
            ErrorType.Conflit => Results.Conflict(error),
            ErrorType.NotFound => Results.NotFound(error),
            ErrorType.ServerError => Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Server Failure",
                type: Enum.GetName(typeof(ErrorType), error.ErrorType),
                extensions: new Dictionary<string, object?> {
                    {  "errors", new[] { error } } 
                }
            ),
            ErrorType.GenericFailure => Results.BadRequest(error),
            _ => Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Server Failure",
                detail: "Something was unexpected",
                type: Enum.GetName(typeof(ErrorType), error.ErrorType),
                extensions: new Dictionary<string, object?> {
                    {  "errors", new[] { error } }
                })

        };
    }
}
