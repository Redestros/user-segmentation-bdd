namespace UserSegmentation.Web.Endpoints.UserEndpoints;

public class CreateUserRequest
{
  public CreateUserRequest(string username, string firstName, string lastName, string email, string phoneNumber)
  {
    Username = username;
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    PhoneNumber = phoneNumber;
  }

  public string Username { get; private set; }
  public string FirstName { get; private set; }
  public string LastName { get; private set; }
  public string Email { get; private set; }
  public string PhoneNumber { get; private set; }
}
