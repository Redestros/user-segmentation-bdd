using FluentValidation;
using LanguageExt.Common;
using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public record AssignSegmentCommand(int UserId, int SegmentId) : IRequest<Result<bool>>;

public class AssignSegmentHandler : IRequestHandler<AssignSegmentCommand, Result<bool>>
{
  private readonly IRepository<Core.UserAggregate.User> _userRepository;
  private readonly IValidator<AssignSegmentCommand> _validator;

  public AssignSegmentHandler(IRepository<Core.UserAggregate.User> userRepository, IValidator<AssignSegmentCommand> validator)
  {
    _userRepository = userRepository;
    _validator = validator;
  }

  public async Task<Result<bool>> Handle(AssignSegmentCommand request, CancellationToken cancellationToken)
  {

    var validationResult = await _validator.ValidateAsync(request, cancellationToken);
    if (!validationResult.IsValid)
    {
      var validationException = new ValidationException(validationResult.Errors);
      return new Result<bool>(validationException);
    }

    var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
    
    user?.AssignToSegment(request.SegmentId);

    await _userRepository.SaveChangesAsync(cancellationToken);
    
    return true;
  }
}
