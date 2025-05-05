namespace UserSegmentation.Application.User;

public static class UserMapper
{
  public static UserDto Map(Core.UserAggregate.User user)
  {
    return new UserDto
    {
      Id = user.Id,
      Username = user.Username,
      FirstName = user.FirstName,
      LastName = user.LastName,
      Email = user.Email,
      PhoneNumber = user.PhoneNumber,
      GrossAnnualRevenue = user.GrossAnnualRevenue,
      SocialScore = user.SocialScore,
      SegmentId = user.SegmentId
    };
  }
}
