namespace UserSegmentation.Application.User;

public class UserDto
{
  public int Id { get; set; }
  public string Username { get; set; } = "";
  public string FirstName { get; set; } = "";
  public string LastName { get; set; } = "";
  public string Email { get; set; } = "";
  public string PhoneNumber { get; set; } = "";
  public Decimal GrossAnnualRevenue { get; set; }
  public int SocialScore { get; set; }
  public int SegmentId { get; set; }
}
