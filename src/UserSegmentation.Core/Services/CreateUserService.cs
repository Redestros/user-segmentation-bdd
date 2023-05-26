using UserSegmentation.Core.Interfaces;
using UserSegmentation.Core.SegmentAggregate;
using UserSegmentation.Core.SegmentAggregate.Specifications;
using UserSegmentation.Core.UserAggregate;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.Services;

public class CreateUserService : ICreateUserService
{
  private readonly IRepository<User> _userRepository;
  private readonly IRepository<Segment> _segmentRepository;

  public CreateUserService(IRepository<User> userRepository, IRepository<Segment> segmentRepository)
  {
    _userRepository = userRepository;
    _segmentRepository = segmentRepository;
  }

  public async Task<int> CreateUser(string username, string email)
  {
    var defaultSegment = await _segmentRepository.FirstOrDefaultAsync(new DefaultSegmentSpecification());

    if (defaultSegment == null)
    {
      throw new Exception("Default segment is null");
    }

    var user = new User(username, email);
    user.AssignToSegment(defaultSegment.Id);

    await _userRepository.AddAsync(user);
    return user.Id;
  }

}
