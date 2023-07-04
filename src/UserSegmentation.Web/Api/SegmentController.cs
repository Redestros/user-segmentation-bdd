using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserSegmentation.Application;
using UserSegmentation.Application.Exceptions;
using UserSegmentation.Application.SegmentFeature;

namespace UserSegmentation.Web.Api;

public class SegmentController : BaseApiController
{
  private readonly IMediator _mediator;

  public SegmentController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet]
  public async Task<ActionResult<SegmentDto>> Get()
  {
    var result = await _mediator.Send(new GetSegmentsQuery());
    return Ok(result);
  }
  
  [HttpGet("{id::int}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _mediator.Send(new GetSegmentByIdQuery(id));
    return result.Match<IActionResult>(Succ: Ok,
      exception =>
      {
        if (exception is SegmentNotFoundException notFoundException)
        {
          return NotFound(notFoundException.ToProblemDetails());
        }
        return StatusCode(500);
      });
  }

  [HttpGet("name")]
  public async Task<IActionResult> GetByName([FromQuery] string name)
  {
    var result = await _mediator.Send(new GetSegmentByNameQuery(name));
    return result.Match<IActionResult>(Succ: Ok,
      exception =>
      {
        if (exception is SegmentNotFoundException notFoundException)
        {
          return NotFound(notFoundException.ToProblemDetails());
        }
        return StatusCode(500);
      });
  }

  [HttpGet("default")]
  public async Task<IActionResult> GetDefault()
  {
    var result = await _mediator.Send(new GetDefaultSegmentQuery());
    return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateSegmentCommand command)
  {
    var result = await _mediator.Send(command);
    return result.Match<IActionResult>(Succ: createdSegmentId =>
        CreatedAtAction(nameof(GetById), new { id = createdSegmentId }, null),
      exception =>
      {
        if (exception is SegmentAlreadyExistsException existsException)
        {
          return Conflict(existsException.ToProblemDetails());
        }
        return StatusCode(500);
      });
  }

  [HttpPost("parameter")]
  public async Task<IActionResult> CreateSegmentParameter([FromBody] CreateSegmentParameterCommand command)
  {
    var result = await _mediator.Send(command);
    return result.Match<IActionResult>(_ => Ok(),
      exception =>
    {
      if (exception is NotFoundException notFoundException)
      {
        return NotFound(notFoundException.ToProblemDetails());
      }

      if (exception is SegmentHasParameterException conflictException)
      {
        return Conflict(conflictException.ToProblemDetails());
      }

      return StatusCode(500);
    });
  }

  [HttpPut("parameter")]
  public async Task<IActionResult> UpdateSegmentParameter([FromBody] UpdateSegmentParameterCommand command)
  {
    var result = await _mediator.Send(command);
    return result.Match<IActionResult>(_ => Ok(), exception =>
    {
      if (exception is NotFoundException notFoundException)
      {
        return NotFound(notFoundException.ToProblemDetails());
      }
      
      return StatusCode(500);
    });
  }
}
