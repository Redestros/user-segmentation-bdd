namespace UserSegmentation.Core.Interfaces;

public interface ICreateUserService
{
  Task<int> CreateUser(string username, string email);
}
