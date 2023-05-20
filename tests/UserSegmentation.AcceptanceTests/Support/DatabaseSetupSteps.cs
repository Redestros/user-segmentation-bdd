using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
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
      var createdUserEntry = _appDbContext?.Set<User>().Add(new User(user.Username, user.Email));
      if (createdUserEntry != null)
      {
        createdUsers.Add(createdUserEntry.Entity);
      }
    }
    _appDbContext?.SaveChanges();
    _scenarioContext.Add("testUsers", createdUsers);
  }
  
}
public record UserInfo(string Username, string Email);
