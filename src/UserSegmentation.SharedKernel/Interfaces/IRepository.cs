using Ardalis.Specification;

namespace UserSegmentation.SharedKernel.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
  bool Exists(Func<T, bool> expression);
}
