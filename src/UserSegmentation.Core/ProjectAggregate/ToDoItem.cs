using Ardalis.GuardClauses;
using UserSegmentation.Core.ProjectAggregate.Events;
using UserSegmentation.SharedKernel;

namespace UserSegmentation.Core.ProjectAggregate;

public class ToDoItem : EntityBase
{
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public int? ContributorId { get; private set; }
  public bool IsDone { get; private set; }

  public void MarkComplete()
  {
    if (!IsDone)
    {
      IsDone = true;

      RegisterDomainEvent(new ToDoItemCompletedEvent(this));
    }
  }

  public void AddContributor(int contributorId)
  {
    Guard.Against.Null(contributorId, nameof(contributorId));
    ContributorId = contributorId;

    var contributorAddedToItem = new ContributorAddedToItemEvent(this, contributorId);
    RegisterDomainEvent(contributorAddedToItem);
  }

  public void RemoveContributor()
  {
    ContributorId = null;
  }

  public override string ToString()
  {
    var status = IsDone ? "Done!" : "Not done.";
    return $"{Id}: Status: {status} - {Title} - {Description}";
  }
}
