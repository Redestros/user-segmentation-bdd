using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.Segment.Queries;

public class GetSegmentsHandler : IRequestHandler<GetSegmentsQuery, List<SegmentDto>>
{
  private readonly IRepository<Core.SegmentAggregate.Segment> _repository;

  public GetSegmentsHandler(IRepository<Core.SegmentAggregate.Segment> repository)
  {
    _repository = repository;
  }

  public async Task<List<SegmentDto>> Handle(GetSegmentsQuery request, CancellationToken cancellationToken)
  {
    var segments = await _repository.ListAsync(cancellationToken);
    return segments.Select(x => new SegmentDto(x.Name)).ToList();
  }
}
