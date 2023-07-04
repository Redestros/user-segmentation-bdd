using Ardalis.Specification;
using UserSegmentation.Core.SegmentAggregate;

namespace UserSegmentation.Core.Services;

public sealed class GetSegmentWithParameterSpec : Specification<Segment>, ISingleResultSpecification<Segment>
{
  public GetSegmentWithParameterSpec(int id)
  {
    Query.Where(x => x.Id == id);
    Query.Include(x => x.SegmentParameters);
    Query.Include(x => x.Parameters);
  }
}
