using Ardalis.Specification;
using UserSegmentation.Core.SegmentAggregate;

namespace UserSegmentation.Core.Services;

public sealed class GetSegmentByNameSpec : Specification<Segment>, ISingleResultSpecification<Segment>
{
  public GetSegmentByNameSpec(string name)
  {
    Query.Where(x => x.Name.Equals(name));
  }
}
