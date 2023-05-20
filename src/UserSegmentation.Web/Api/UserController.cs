#region
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserSegmentation.Application;
using UserSegmentation.Application.User;

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

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
  {
    var response = await _mediator.Send(new CreateUserCommand(request.Username, request.Email));
    return response.Match<IActionResult>(createdUserResponse => CreatedAtAction("Get", new { Id = createdUserResponse.Id }, null), exception =>
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
      request.PhoneNumber);
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
}
