﻿using LanguageExt.Common;
using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public record GetUserQuery(int Id) : IRequest<Result<UserDto>>;
public class GetUserHandler : IRequestHandler<GetUserQuery, Result<UserDto>>
{
  private readonly IRepository<Core.UserAggregate.User> _repository;

  public GetUserHandler(IRepository<Core.UserAggregate.User> repository)
  {
    _repository = repository;
  }

  public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (user == null)
    {
      return new Result<UserDto>(new UserNotFoundException(request.Id));
    }

    var userDto = UserMapper.Map(user);
    return new Result<UserDto>(userDto);
  }
}
