using LanguageExt.Common;
using MediatR;
using UserSegmentation.Application.ParameterFeature;
using UserSegmentation.Application.SegmentFeature;
using UserSegmentation.Core.SegmentAggregate;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public record OverrideParameterCommand(int UserId, int ParameterId, string Value) : IRequest<Result<bool>>;
public class OverrideParameterHandler : IRequestHandler<OverrideParameterCommand, Result<bool>>
{
  private readonly IRepository<Core.UserAggregate.User> _userRepository;
  private readonly IRepository<Segment> _segmentRepository;

  public OverrideParameterHandler(IRepository<Core.UserAggregate.User> userRepository, IRepository<Segment> segmentRepository)
  {
    _userRepository = userRepository;
    _segmentRepository = segmentRepository;
  }

  public async Task<Result<bool>> Handle(OverrideParameterCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
    if (user is null)
    {
      return new Result<bool>(new UserNotFoundException(request.UserId));
    }

    var segment = await _segmentRepository.GetByIdAsync(user.SegmentId, cancellationToken);

    if (segment is null)
    {
      return new Result<bool>(new SegmentNotFoundException(request.UserId)); 
    }

    var segmentParameter = segment.SegmentParameters.Find(x => x.ParameterId == request.ParameterId);
    if (segmentParameter is null)
    {
      return new Result<bool>(new ParameterNotFoundException(request.ParameterId));
    }

    segmentParameter.SetValue(request.Value);

    await _segmentRepository.SaveChangesAsync(cancellationToken);
    return true;
  }
}
