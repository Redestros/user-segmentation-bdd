using UserSegmentation.SharedKernel;

namespace UserSegmentation.Core.UserAggregate.Events;

public class UsedAssignedToSegmentEvent : DomainEventBase
{
  public User User { get; private set; }
  public int SegmentId { get; private set; }

  public UsedAssignedToSegmentEvent(User user, int segmentId)
  {
    User = user;
    SegmentId = segmentId;
  }
}
