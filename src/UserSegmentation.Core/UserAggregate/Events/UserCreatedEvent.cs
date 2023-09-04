using UserSegmentation.SharedKernel;

namespace UserSegmentation.Core.UserAggregate.Events;

public class UserCreatedEvent : DomainEventBase
{
  public UserCreatedEvent(User user)
  {
    User = user;
  }
  
  public User User { get; private set; }
}
