using Ardalis.Specification;
using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public record SearchUserQuery(string Username) : IRequest<List<UserDto>>;
public class SearchUserHandler : IRequestHandler<SearchUserQuery, List<UserDto>>
{

  private readonly IRepository<Core.UserAggregate.User> _repository;

  public SearchUserHandler(IRepository<Core.UserAggregate.User> repository)
  {
    _repository = repository;
  }

  public async Task<List<UserDto>> Handle(SearchUserQuery request, CancellationToken cancellationToken)
  {
    var users = await _repository.ListAsync(new SearchUserSpec(request.Username), 
      cancellationToken);
    return users.Select(UserMapper.Map).ToList();
  }
  
  private sealed class SearchUserSpec : Specification<Core.UserAggregate.User>
  {
    public SearchUserSpec(string username)
    {
      Query.Where(x => x.Username.Contains(username));
    }
  }
}
