using MediatR;
using UserSegmentation.Core.Services;
using UserSegmentation.Core.UserAggregate.Events;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.UserAggregate.Handlers;

public class UsedAssignedToSegmentHandler : INotificationHandler<UsedAssignedToSegmentEvent>
{
  private readonly UserSegmentService _userSegmentService;

  public UsedAssignedToSegmentHandler(UserSegmentService segmentService)
  {
    _userSegmentService = segmentService;
  }

  public async Task Handle(UsedAssignedToSegmentEvent domainEvent, CancellationToken cancellationToken)
  {
    await _userSegmentService.Assign(domainEvent.UserId, domainEvent.SegmentId);
  }
}
