using UserSegmentation.Core.UserAggregate;

namespace UserSegmentation.Core.Interfaces;

public interface IUserParameterRepository
{
  List<UserParameterInfo> Get(int userId);
}
