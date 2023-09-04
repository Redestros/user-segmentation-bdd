using UserSegmentation.Core.UserAggregate.Events;
using UserSegmentation.Core.UserAggregate.Exceptions;
using UserSegmentation.SharedKernel;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.UserAggregate;

public class User : EntityBase, IAggregateRoot
{
  public string Username { get; private set; }
  public string FirstName { get; private set; } = "";
  public string LastName { get; private set; } = "";
  public string Email { get; private set; }
  public string PhoneNumber { get; private set; } = "";
  public DateOnly Birthdate { get; private set; }
  public Decimal GrossAnnualRevenue { get; private set; }
  public int SocialScore { get; set; }
  public int SegmentId { get; private set; }

  public User(string username, string email)
  {
    Username = username;
    Email = email;
  }

  public User(string username, string firstName, string lastName, string email, string phoneNumber, DateOnly birthdate)
  {
    Username = username;
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    PhoneNumber = phoneNumber;
    Birthdate = birthdate;
    RegisterDomainEvent(new UserCreatedEvent(this));
  }

  public void UpdatePersonalInfo(string firstName, string lastName, string phoneNumber)
  {
    FirstName = firstName;
    LastName = lastName;
    PhoneNumber = phoneNumber;
  }

  public void UpdateFinancialInfo(Decimal grossAnnualRevenue, int socialScore)
  {
    if (grossAnnualRevenue < this.GrossAnnualRevenue)
    {
      throw new DecreaseRevenueException();
    }
    GrossAnnualRevenue = grossAnnualRevenue;
    SocialScore = socialScore;
    RegisterDomainEvent(new FinancialInfoUpdatedEvent(this));
  }

  public void AssignToSegment(int segmentId)
  {
    SegmentId = segmentId;
    RegisterDomainEvent(new UsedAssignedToSegmentEvent(this, segmentId));
  }

  public bool IsAboveEighteen()
  {
    DateOnly today = DateOnly.FromDateTime(DateTime.Today);
    int age = today.Year - Birthdate.Year;

    if (Birthdate > today.AddYears(-age))
    {
      age--;
    }

    return age > 18;
  }
}
