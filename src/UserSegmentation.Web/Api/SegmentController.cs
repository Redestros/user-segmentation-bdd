using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserSegmentation.Application.Segment.commands;
using UserSegmentation.Application.Segment.Queries;

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

  [HttpPost]
  public async Task<ActionResult> Create([FromBody] CreateSegmentCommand command)
  {
    var createdSegmentId = await _mediator.Send(command);
    return CreatedAtAction(nameof(Get), new { id = createdSegmentId });
  }
}
