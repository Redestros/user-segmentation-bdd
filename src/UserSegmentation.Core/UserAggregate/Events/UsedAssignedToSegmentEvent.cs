using UserSegmentation.SharedKernel;

namespace UserSegmentation.Core.UserAggregate.Events;

public class UsedAssignedToSegmentEvent : DomainEventBase
{
  public int UserId { get; private set; }
  public SegmentReference SegmentReference { get; private set; }

  public UsedAssignedToSegmentEvent(int userId, SegmentReference segmentReference)
  {
    UserId = userId;
    SegmentReference = segmentReference;
  }
}
