﻿using Ardalis.Specification;

namespace UserSegmentation.Core.SegmentAggregate.Specifications;

public sealed class SegmentByNameSpec : Specification<Segment>, ISingleResultSpecification
{
  public SegmentByNameSpec(string segmentName)
  {
    Query.Where(segment => segment.Name.Equals(segmentName));
  }
}
