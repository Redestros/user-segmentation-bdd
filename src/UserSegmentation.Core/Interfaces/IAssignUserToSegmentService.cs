using Ardalis.Result;
using UserSegmentation.Core.UserAggregate;

namespace UserSegmentation.Core.Interfaces;

public interface IAssignUserToSegmentService
{
  public Task<Result> Assign(int userId, SegmentReference reference);
}
