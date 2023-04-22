using Ardalis.Result;
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
  (int Id, string FirstName, string LastName, string PhoneNumber) : IRequest<Result>;

public class EditUserPersonalInfoHandler : IRequestHandler<UpdateUserPersonalInfoCommand, Result>
{
  private IRepository<Core.UserAggregate.User> _repository;

  public EditUserPersonalInfoHandler(IRepository<Core.UserAggregate.User> repository)
  {
    _repository = repository;
  }

  public async Task<Result> Handle(UpdateUserPersonalInfoCommand request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (user == null)
    {
      return Result.NotFound();
    }

    user.UpdatePersonalInfo(request.FirstName, request.LastName, request.PhoneNumber);
    await _repository.UpdateAsync(user, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}
