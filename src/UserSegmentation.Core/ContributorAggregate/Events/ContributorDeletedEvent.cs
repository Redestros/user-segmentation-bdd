using UserSegmentation.SharedKernel;

namespace UserSegmentation.Core.ContributorAggregate.Events;

public class ContributorDeletedEvent : DomainEventBase
{
  public int ContributorId { get; set; }

  public ContributorDeletedEvent(int contributorId)
  {
    ContributorId = contributorId;
  }
}
