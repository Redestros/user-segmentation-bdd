namespace UserSegmentation.Core.UserAggregate;

public class UserParameter
{
  public int UserId { get; private set; }
  public int ParameterId { get; private set; }
  public string Value { get; private set; }
  
  public UserParameter(int userId, int parameterId, string value)
  {
    this.UserId = userId;
    this.ParameterId = parameterId;
    this.Value = value;
  }
}
