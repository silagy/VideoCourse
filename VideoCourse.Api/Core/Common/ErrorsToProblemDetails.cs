using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace VideoCourse.Api.Core.Common;

public static class ErrorsToProblemDetails
{
    public static ProblemDetails ToProblemDetails(this List<Error> errors)
    {
        var problem = new ProblemDetails();
        
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            //return ValidationProblem(errors);
        }
        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status417ExpectationFailed,
            ErrorType.Unexpected => StatusCodes.Status417ExpectationFailed,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        problem.Status = statusCode;
        problem.Title = firstError.Code;
        problem.Type = firstError.Type.ToString();
        problem.Detail = firstError.Description;

        return problem;
    }
}