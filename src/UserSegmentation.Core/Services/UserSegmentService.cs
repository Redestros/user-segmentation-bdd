using Ardalis.Result;
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

  public async Task<Result> Assign(int userId, int segmentId)
  {
    var user = await _userRepository.GetByIdAsync(userId);
    if (user == null)
    {
      return Result.NotFound("User not found");
    }

    var segment = await _segmentRepository.GetByIdAsync(segmentId);
    if (segment == null)
    {
      return Result.NotFound("Segment not found");
    }

    if (segment.Name.ToLower().Equals("default"))
    {
      return Result.Error("Cannot assign user to default segment");
    }

    user.AssignToSegment(segmentId);
    await _userRepository.UpdateAsync(user);

    return Result.Success();
  }
}
