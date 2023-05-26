using MediatR;
using UserSegmentation.Core.Services;

namespace UserSegmentation.Application.Segment;

public record GetDefaultSegmentQuery : IRequest<SegmentDto>;

public class GetDefaultSegmentHandler : IRequestHandler<GetDefaultSegmentQuery, SegmentDto>
{
  private readonly SegmentService _segmentService;

  public GetDefaultSegmentHandler(SegmentService segmentService)
  {
    _segmentService = segmentService;
  }

  public async Task<SegmentDto> Handle(GetDefaultSegmentQuery request, CancellationToken cancellationToken)
  {
    var defaultSegment = await _segmentService.GetDefaultSegment();

    return new SegmentDto(defaultSegment.Id, defaultSegment.Name);
  }
}
