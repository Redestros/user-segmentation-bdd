using Ardalis.Result;
using UserSegmentation.Core.Interfaces;
using UserSegmentation.Core.SegmentAggregate;
using UserSegmentation.Core.UserAggregate;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.Services;

public class AssignUserToSegmentService : IAssignUserToSegmentService
{
  private readonly IRepository<User> _userRepository;
  private readonly IRepository<Segment> _segmentRepository;

  public AssignUserToSegmentService(IRepository<User> userRepository, IRepository<Segment> segmentRepository)
  {
    _userRepository = userRepository;
    _segmentRepository = segmentRepository;
  }

  public async Task<Result> Assign(int userId, SegmentReference reference)
  {
    var user = await _userRepository.GetByIdAsync(userId);
    if (user == null)
    {
      return Result.NotFound("User not found");
    }

    var segment = await _segmentRepository.GetByIdAsync(reference.Id);
    if (segment == null)
    {
      return Result.NotFound("Segment not found");
    }

    if (segment.Name.Equals("Default"))
    {
      return Result.Error("Cannot assign user to default segment");
    }

    user.AssignToSegment(reference);
    await _userRepository.UpdateAsync(user);

    return Result.Success();
  }
}
