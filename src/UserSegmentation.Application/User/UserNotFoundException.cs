using UserSegmentation.Application.Exceptions;

namespace UserSegmentation.Application.User;

public class UserNotFoundException : NotFoundException
{
  public UserNotFoundException(int id) : base($"User with id {id} not found") 
  {
    
  }
}
