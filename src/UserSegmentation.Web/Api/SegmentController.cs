using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserSegmentation.Application;
using UserSegmentation.Application.Segment;

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
}
