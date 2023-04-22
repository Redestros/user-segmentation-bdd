using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserSegmentation.Application.User;

namespace UserSegmentation.Web.Api;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
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

  [HttpPost]
  public async Task<ActionResult> Create([FromBody] CreateUserRequest request)
  {
    var id = await _mediator.Send(new CreateUserCommand(request.Username, request.Email));
    return Ok(id);
  }

  [HttpPut("{id:int}")]
  public async Task<Result> UpdatePersonalInfo(int id, [FromBody] UpdateUserPersonalInfoRequest request)
  {
    var command =
      new UpdateUserPersonalInfoCommand(
        id,
        request.FirstName,
        request.LastName,
        request.PhoneNumber);
    var result = await _mediator.Send(command);
    return result;
  }
}
