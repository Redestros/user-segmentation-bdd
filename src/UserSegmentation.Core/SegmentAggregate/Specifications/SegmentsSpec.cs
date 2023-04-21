using Ardalis.Specification;

namespace UserSegmentation.Core.SegmentAggregate.Specifications;

public sealed class SegmentsSpec : Specification<Segment>
{
  public SegmentsSpec()
  {
    Query.Skip(0).Take(10);
  }
}
