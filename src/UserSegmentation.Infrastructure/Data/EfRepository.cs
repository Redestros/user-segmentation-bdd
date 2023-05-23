using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
  private readonly AppDbContext _appDbContext;
  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
    _appDbContext = dbContext;
  }

  public bool Exists(Func<T, bool> expression)
  {
    return _appDbContext.Set<T>().Exists(expression);
  }
  
}
