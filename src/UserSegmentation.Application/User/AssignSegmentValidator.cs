using FluentValidation;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public class AssignSegmentValidator : AbstractValidator<AssignSegmentCommand>
{
  private readonly IRepository<Core.UserAggregate.User> _userRepository;
  private readonly IRepository<Core.SegmentAggregate.Segment> _segmentRepository;

  public AssignSegmentValidator(IRepository<Core.UserAggregate.User> userRepository, IRepository<Core.SegmentAggregate.Segment> segmentRepository)
  {
    _userRepository = userRepository;
    _segmentRepository = segmentRepository;

    RuleFor(x => x.UserId)
      .NotEmpty()
      .WithMessage("User Id is required")
      .Must((CheckUserExists));
    
    RuleFor(x => x.SegmentId)
      .NotEmpty()
      .WithMessage("Segment Id is required")
      .Must((CheckSegmentExists));
  }


  private bool CheckUserExists(int userId)
  {
    return _userRepository.Exists(x => x.Id == userId);
  }

  private bool CheckSegmentExists(int segmentId)
  {
    return _segmentRepository.Exists(x => x.Id == segmentId);
  }
}
