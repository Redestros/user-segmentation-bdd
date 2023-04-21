using MediatR;
using UserSegmentation.Core.UserAggregate.Events;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.UserAggregate.Handlers;

public class UsedAssignedToSegmentHandler : INotificationHandler<UsedAssignedToSegmentEvent>
{
  private readonly IRepository<User> _repository;

  public UsedAssignedToSegmentHandler(IRepository<User> repository)
  {
    _repository = repository;
  }

  public async Task Handle(UsedAssignedToSegmentEvent domainEvent, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(domainEvent.UserId, cancellationToken);
  }
}
