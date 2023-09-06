using MediatR;
using UserSegmentation.Core.Services;
using UserSegmentation.Core.UserAggregate.Events;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.UserAggregate.Handlers;

public class UpdateUserSegment : INotificationHandler<FinancialInfoUpdatedEvent>
{
  private readonly SegmentAssignmentEngine _assignmentEngine;
  private readonly IRepository<User> _repository;

  public UpdateUserSegment(SegmentAssignmentEngine assignmentEngine, IRepository<User> repository)
  {
    _assignmentEngine = assignmentEngine;
    _repository = repository;
  }

  public async Task Handle(FinancialInfoUpdatedEvent financialInfoUpdatedEvent, CancellationToken cancellationToken)
  {
    var user = financialInfoUpdatedEvent.User;

    var segmentId = await _assignmentEngine.GetSegmentToAssign(user);

    user.AssignToSegment(segmentId);

    await _repository.UpdateAsync(user, cancellationToken);
  }
}
