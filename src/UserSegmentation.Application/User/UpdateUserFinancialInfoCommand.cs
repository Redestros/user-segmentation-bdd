﻿using LanguageExt.Common;
using MediatR;
using UserSegmentation.Core.UserAggregate.Exceptions;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public record UpdateUserFinancialInfoCommand(int Id, Decimal GrossAnnualRevenue, int SocialScore) : IRequest<Result<bool>>;

public class UpdateUserFinancialInfoHandler: IRequestHandler<UpdateUserFinancialInfoCommand, Result<bool>>
{
  private readonly IRepository<Core.UserAggregate.User> _repository;

  public UpdateUserFinancialInfoHandler(IRepository<Core.UserAggregate.User> repository)
  {
    _repository = repository;
  }

  public async Task<Result<bool>> Handle(UpdateUserFinancialInfoCommand request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (user == null)
    {
      return new Result<bool>(new UserNotFoundException(request.Id));
    }

    try
    {
      user.UpdateFinancialInfo(request.GrossAnnualRevenue, request.SocialScore);
    }
    catch (DecreaseRevenueException decreaseRevenueException)
    {
      return new Result<bool>(decreaseRevenueException);
    }

    await _repository.SaveChangesAsync(cancellationToken);

    return true;
  }
}