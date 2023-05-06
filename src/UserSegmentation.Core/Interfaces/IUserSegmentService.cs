using LanguageExt.Common;

namespace UserSegmentation.Core.Interfaces;

public interface IUserSegmentService
{
  public Task<Result<bool>> Assign(int userId, int segmentId);
}
