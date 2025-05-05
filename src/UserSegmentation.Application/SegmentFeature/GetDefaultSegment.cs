using MediatR;
using UserSegmentation.Core.Interfaces;

namespace UserSegmentation.Application.SegmentFeature;

public record GetDefaultSegmentQuery : IRequest<SegmentDto>;

public class GetDefaultSegmentHandler : IRequestHandler<GetDefaultSegmentQuery, SegmentDto>
{
  private readonly ISegmentService _segmentService;

  public GetDefaultSegmentHandler(ISegmentService segmentService)
  {
    _segmentService = segmentService;
  }

  public async Task<SegmentDto> Handle(GetDefaultSegmentQuery request, CancellationToken cancellationToken)
  {
    var defaultSegment = await _segmentService.GetDefaultSegment();

    return new SegmentDto(defaultSegment.Id, defaultSegment.Name);
  }
}
