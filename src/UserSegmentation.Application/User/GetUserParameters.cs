using LanguageExt.Common;
using MediatR;
using UserSegmentation.Core.Interfaces;
using UserSegmentation.Core.UserAggregate;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public record GetUserParametersQuery(int UserId) : IRequest<Result<List<UserParameter>>>;

public class GetUserParametersHandler : IRequestHandler<GetUserParametersQuery, Result<List<UserParameter>>>
{
  private readonly IRepository<Core.UserAggregate.User> _userRepository;

  private readonly ISegmentRepository _segmentRepository;

  public GetUserParametersHandler(IRepository<Core.UserAggregate.User> userRepository,
    ISegmentRepository segmentRepository)
  {
    _userRepository = userRepository;
    _segmentRepository = segmentRepository;
  }

  public async Task<Result<List<UserParameter>>> Handle(GetUserParametersQuery request,
    CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
    if (user is null)
    {
      return new Result<List<UserParameter>>(new UserNotFoundException(request.UserId));
    }

    return _segmentRepository.GetParameters(user.SegmentId);
  }
}

