namespace UserSegmentation.AcceptanceTests.Models;

public class CreateUserRequest
{
  public string Username { get; set; } = "";
  public string Email { get; set; } = "";
}

public record CreateUserResponse(int Id);

public record CreateUserCommand(string Username, string Email);

public class UserDto
{
  public int Id { get; set; }
  public string Username { get; set; } = "";
  public string FirstName { get; set; } = "";
  public string LastName { get; set; } = "";
  public string Email { get; set; } = "";
  public string PhoneNumber { get; set; } = "";

}
