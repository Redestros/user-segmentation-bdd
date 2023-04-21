using Ardalis.GuardClauses;
using UserSegmentation.SharedKernel;

namespace UserSegmentation.Core.UserAggregate;

public class SegmentReference : ValueObject
{
  public SegmentAssignment Assignment { get; private set; }
  public int Id { get; private set; }

  public SegmentReference(SegmentAssignment assignment, int id)
  {
    Assignment = Guard.Against.EnumOutOfRange(assignment);
    Id = Guard.Against.Zero(id);
  }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return Assignment;
    yield return Id;
  }
}
