using Ardalis.Specification;
using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.Segment.Queries;

public class GetSegmentHandler : IRequestHandler<GetSegmentQuery, SegmentDto>
{
  private readonly IRepository<Core.SegmentAggregate.Segment> _repository;

  public GetSegmentHandler(IRepository<Core.SegmentAggregate.Segment> repository)
  {
    _repository = repository;
  }

  public async Task<SegmentDto> Handle(GetSegmentQuery request, CancellationToken cancellationToken)
  {
    var segment = await _repository.FirstOrDefaultAsync(
      new SegmentSpec(request), cancellationToken) 
                  ?? throw new Exception("Segment NotFound");

    return new SegmentDto(segment.Name);
  }
}

public sealed class SegmentSpec : Specification<Core.SegmentAggregate.Segment>
{
  public SegmentSpec(GetSegmentQuery query)
  {
    Query.Where(s => s.Name.Equals(query.Name));
  }
}
