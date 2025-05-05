using System.Net.Http.Json;
using TechTalk.SpecFlow;
using UserSegmentation.AcceptanceTests.Support;
using UserSegmentation.Application.SegmentFeature;
using UserSegmentation.Application.User;
using Xunit;

namespace UserSegmentation.AcceptanceTests.Steps;

[Binding]
public class SegmentAssignmentSteps
{
  private const string UserEndpoint = "api/user";
  private const string Username = "Draven";
  private const string SegmentEndpoint = "api/segment";
  private readonly HttpClient _httpClient;
  private readonly ScenarioContext _context;


  public SegmentAssignmentSteps(ScenarioContext context, ApplicationFactory factory)
  {
    _context = context;
    _httpClient = factory.CreateDefaultClient(new Uri($"http://localhost/"));
  }

  [Given(@"an existing user born on (.*)")]
  public async Task GivenAnExistingUserBornOn(DateTime birthdate)
  {
    var birthdateValue = DateOnly.FromDateTime(birthdate);
    var createUserCommand = new CreateUserCommand(
      Username,
      "Draven",
      "Executioner",
      "draven@gmail.com",
      "25994801",
      birthdateValue);
    await _httpClient.PostAsJsonAsync($"{UserEndpoint}", createUserCommand);
  }

  [When(@"he update his financials including (.*) and (.*)")]
  public async Task WhenHeUpdateHisFinancialsIncludingAnd(Decimal grossAnnualRevenue, int socialScore)
  {
    var users = await _httpClient.GetFromJsonAsync<List<UserDto>>($"{UserEndpoint}" +
                                                                  $"/{Username}");
    if (users == null)
    {
      throw new Exception();
    }

    var user = users.FirstOrDefault();
    var updateFinancialRequest = new UpdateUserFinancialsRequest(
      grossAnnualRevenue,
      socialScore
    );
    _context.Add("userId", user?.Id);
    await _httpClient.PutAsJsonAsync($"{UserEndpoint}/{user?.Id}/financials", updateFinancialRequest);
  }

  [Then(@"he should be assigned to a (.*)")]
  public async Task ThenHeShouldBeAssignedToA(string segment)
  {
    var id = _context.Get<int>("userId");
    var updatedUser = await _httpClient.GetFromJsonAsync<UserDto>($"{UserEndpoint}/{id}");
    var updateUserSegmentId = updatedUser?.SegmentId;
    var existingSegment = await _httpClient.GetFromJsonAsync<SegmentDto>($"{SegmentEndpoint}/{updateUserSegmentId}");
    Assert.NotNull(existingSegment);
    Assert.Equal(segment, existingSegment.Name);
  }
}
