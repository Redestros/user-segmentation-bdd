using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UserSegmentation.Application.SegmentFeature;
using UserSegmentation.Core.SegmentAggregate;
using UserSegmentation.Core.UserAggregate;
using UserSegmentation.Infrastructure.Data;

namespace UserSegmentation.AcceptanceTests.Support;

[Binding]
public class DatabaseSetupSteps
{
  private readonly ScenarioContext _scenarioContext;
  private readonly AppDbContext? _appDbContext;

  public DatabaseSetupSteps(ApplicationFactory applicationFactory, ScenarioContext scenarioContext)
  {
    _scenarioContext = scenarioContext;
    _appDbContext = (AppDbContext)applicationFactory.Services.GetService(typeof(AppDbContext))!;
  }

  [Given(@"the following users")]
  public void GivenTheFollowingUsers(Table table)
  {
    var users = table.CreateSet<UserInfo>();
    var createdUsers = new List<User>();
    foreach (var user in users)
    {
      var createdUserEntry = _appDbContext?.Set<User>().Add(new User(
        user.Username, "", "", "", "", DateOnly.FromDateTime(DateTime.Today)));
      if (createdUserEntry != null)
      {
        createdUsers.Add(createdUserEntry.Entity);
      }
    }
    _appDbContext?.SaveChanges();
    _scenarioContext.Add("testUsers", createdUsers);
  }

  [Given(@"the following segments")]
  public void GivenTheFollowingSegments(Table table)
  {
    var segments = table.CreateSet<SegmentDto>();
    var createdSegments = new List<Segment>();
    foreach (var segment in segments)
    {
      var segmentEntry = _appDbContext?.Set<Segment>().Add(new Segment(segment.Name));
      if (segmentEntry != null)
      {
        createdSegments.Add(segmentEntry.Entity);
      }
    }

    _appDbContext?.SaveChanges();
    _scenarioContext.Add("testSegments", createdSegments);
  }
}
public record UserInfo(string Username, string Email);
