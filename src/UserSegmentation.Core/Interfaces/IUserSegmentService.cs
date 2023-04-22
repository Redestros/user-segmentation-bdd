using Ardalis.Result;
using UserSegmentation.Core.UserAggregate;

namespace UserSegmentation.Core.Interfaces;

public interface IUserSegmentService
{
  public Task<Result> Assign(int userId, int segmentId);
}
