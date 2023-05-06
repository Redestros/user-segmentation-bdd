using LanguageExt.Common;
using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public class UpdateUserPersonalInfoRequest
{
  public string FirstName { get; set; } = "";
  public string LastName { get; set; } = "";
  public string PhoneNumber { get; set; } = "";
}

public record UpdateUserPersonalInfoCommand
  (int Id, string FirstName, string LastName, string PhoneNumber) : IRequest<Result<bool>>;

public class UpdateUserPersonalInfoHandler : IRequestHandler<UpdateUserPersonalInfoCommand, Result<bool>>
{
  private readonly IRepository<Core.UserAggregate.User> _repository;

  public UpdateUserPersonalInfoHandler(IRepository<Core.UserAggregate.User> repository)
  {
    _repository = repository;
  }

  public async Task<Result<bool>> Handle(UpdateUserPersonalInfoCommand request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (user == null)
    {
      return new Result<bool>(new UserNotFoundException(request.Id));
    }

    user.UpdatePersonalInfo(request.FirstName, request.LastName, request.PhoneNumber);
    await _repository.UpdateAsync(user, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return true;
  }
}
