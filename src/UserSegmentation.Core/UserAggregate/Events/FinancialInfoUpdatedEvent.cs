using UserSegmentation.SharedKernel;

namespace UserSegmentation.Core.UserAggregate.Events;

public class FinancialInfoUpdatedEvent : DomainEventBase
{
  public FinancialInfoUpdatedEvent(User user)
  {
    User = user;
  }  
  
  public User User { get; private set; }

}
