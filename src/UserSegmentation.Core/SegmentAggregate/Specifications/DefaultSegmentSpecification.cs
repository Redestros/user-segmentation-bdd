using Ardalis.Specification;

namespace UserSegmentation.Core.SegmentAggregate.Specifications;

public sealed class DefaultSegmentSpecification : Specification<Segment>, ISingleResultSpecification
{
  public DefaultSegmentSpecification()
  {
    Query.Where(x => x.Name.ToLower().Equals("default"));
  }
}
