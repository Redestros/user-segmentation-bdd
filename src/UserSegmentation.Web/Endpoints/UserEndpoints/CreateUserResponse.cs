namespace UserSegmentation.Web.Endpoints.UserEndpoints;

public class CreateUserResponse
{
  public CreateUserResponse(int id, string username)
  {
    Id = id;
    Username = username;
  }

  public int Id { get; private set; }
  public string Username { get; private set; }
}
