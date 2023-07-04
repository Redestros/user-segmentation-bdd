using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserSegmentation.Application.ParameterFeature;

namespace UserSegmentation.Web.Api;

public class ParameterController : BaseApiController
{
  private readonly IMediator _mediator;

  public ParameterController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet]
  public async Task<IActionResult> Get()
  {
    var results = await _mediator.Send(new GetParametersRequest());
    return Ok(results);
  }
}
