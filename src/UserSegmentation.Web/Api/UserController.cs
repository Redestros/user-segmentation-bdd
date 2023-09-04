#region

using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserSegmentation.Application;
using UserSegmentation.Application.Exceptions;
using UserSegmentation.Application.User;
using UserSegmentation.Core.UserAggregate.Exceptions;

#endregion

namespace UserSegmentation.Web.Api;

public class UserController : BaseApiController
{
  private readonly IMediator _mediator;

  public UserController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet]
  public async Task<ActionResult> Get()
  {
    var result = await _mediator.Send(new GetUsersQuery());
    return Ok(result);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> Get(int id)
  {
    var result = await _mediator.Send(new GetUserQuery(id));
    return result.Match<IActionResult>(Ok, exception =>
    {
      if (exception is UserNotFoundException userNotFoundException)
      {
        return NotFound(userNotFoundException.ToProblemDetails());
      }

      return StatusCode(500);
    });
  }

  [HttpGet("{id:int}/parameters")]
  public async Task<IActionResult> GetParameters(int id)
  {
    var result = await _mediator.Send(new GetUserParametersQuery(id));
    return result.Match<IActionResult>(Ok, exception =>
    {
      if (exception is NotFoundException notFoundException)
      {
        return NotFound(notFoundException.ToProblemDetails());
      }

      return StatusCode(500);
    });
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
  {
    var response = await _mediator.Send(new CreateUserCommand(
      request.Username, request.Firstname, request.Lastname, request.Email, request.PhoneNumber, request.Birthdate));

    return response.Match<IActionResult>(
      createdUserResponse => CreatedAtAction("Get", new { createdUserResponse.Id }, null), exception =>
      {
        if (exception is ValidationException validationException)
        {
          return BadRequest(validationException.ToProblemDetails());
        }

        return StatusCode(500);
      });
  }

  [HttpPut("{id:int}")]
  public async Task<IActionResult> UpdatePersonalInfo(int id, [FromBody] UpdateUserPersonalInfoRequest request)
  {
    var command = new UpdateUserPersonalInfoCommand(
      id,
      request.FirstName,
      request.LastName,
      request.PhoneNumber
    );

    var result = await _mediator.Send(command);

    return result.Match<IActionResult>(_ => Ok(), exception =>
    {
      if (exception is UserNotFoundException userNotFoundException)
      {
        return NotFound(userNotFoundException.ToProblemDetails());
      }

      return StatusCode(500);
    });
  }

  [HttpPut("segment")]
  public async Task<IActionResult> AssignSegment([FromBody] AssignSegmentCommand command)
  {
    var result = await _mediator.Send(command);

    return result.Match<IActionResult>(_ => Ok(), exception =>
    {
      if (exception is ValidationException validationException)
      {
        return BadRequest(validationException.ToProblemDetails());
      }

      return StatusCode(500);
    });
  }

  [HttpPut("financials")]
  public async Task<IActionResult> UpdateFinancialInfo([FromBody] UpdateUserFinancialInfoCommand command)
  {
    var result = await _mediator.Send(command);

    return result.Match<IActionResult>(_ => Ok(), exception =>
    {
      return exception switch
      {
        UserNotFoundException notFoundException => NotFound(notFoundException.ToProblemDetails()),
        DecreaseRevenueException revenueException => BadRequest(revenueException.ToProblemDetails()),
        _ => StatusCode(500)
      };
    });
  }
}
