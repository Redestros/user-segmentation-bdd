using LanguageExt.Common;
using UserSegmentation.Core.Interfaces;
using UserSegmentation.Core.SegmentAggregate;
using UserSegmentation.Core.UserAggregate;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.Services;

public class UserSegmentService : IUserSegmentService
{
  private readonly IRepository<User> _userRepository;
  private readonly IRepository<Segment> _segmentRepository;

  public UserSegmentService(IRepository<User> userRepository, IRepository<Segment> segmentRepository)
  {
    _userRepository = userRepository;
    _segmentRepository = segmentRepository;
  }

  public async Task<Result<bool>> Assign(int userId, int segmentId)
  {
    var user = await _userRepository.GetByIdAsync(userId);
    if (user == null)
    {
      return new Result<bool>(new Exception("user not found"));
    }

    var segment = await _segmentRepository.GetByIdAsync(segmentId);
    if (segment == null)
    {
      return new Result<bool>(new Exception("segment not found"));
    }

    if (segment.Name.ToLower().Equals("default"))
    {
      return new Result<bool>(new Exception("Cannot assign user to default segment"));
    }

    user.AssignToSegment(segmentId);
    await _userRepository.UpdateAsync(user);

    return true;
  }
}
