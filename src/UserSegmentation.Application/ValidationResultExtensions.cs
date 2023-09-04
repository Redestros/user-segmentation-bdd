using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UserSegmentation.Application.Exceptions;
using UserSegmentation.Core.UserAggregate.Exceptions;

namespace UserSegmentation.Application;

public static class ValidationResultExtensions
{
  public static ProblemDetails ToProblemDetails(this ValidationException exception)
  {
    var errors = exception.Errors
      .ToDictionary(x => x.PropertyName, x => x.ErrorMessage);
    var problemDetails = new ProblemDetails
    {
      Type = "ValidationError",
      Detail = "Invalid request, please check the error list for more details",
      Status = (int)(HttpStatusCode.BadRequest),
      Title = "Invalid request"
    };

    problemDetails.Extensions.Add("errors", errors);
    return problemDetails;
  }

  public static ProblemDetails ToProblemDetails(this NotFoundException exception)
  {
    var message = exception.Message;
    var problemDetails = new ProblemDetails { Status = (int)(HttpStatusCode.NotFound), Title = message };
    return problemDetails;
  }

  public static ProblemDetails ToProblemDetails(this ConflictException exception)
  {
    var message = exception.Message;
    var problemDetails = new ProblemDetails { Status = (int)(HttpStatusCode.Conflict), Title = message };
    return problemDetails;
  }

  public static ProblemDetails ToProblemDetails(this DecreaseRevenueException exception)
  {
    var message = exception.Message;
    var problemDetails = new ProblemDetails { Status = (int)(HttpStatusCode.BadRequest), Title = message };
    return problemDetails;
  }
}
