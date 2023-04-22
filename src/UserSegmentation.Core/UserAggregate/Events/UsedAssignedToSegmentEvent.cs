using UserSegmentation.SharedKernel;

namespace UserSegmentation.Core.UserAggregate.Events;

public class UsedAssignedToSegmentEvent : DomainEventBase
{
  public int UserId { get; private set; }
  public int SegmentId { get; private set; }

  public UsedAssignedToSegmentEvent(int userId, int segmentId)
  {
    UserId = userId;
    SegmentId = segmentId;
  }
}
