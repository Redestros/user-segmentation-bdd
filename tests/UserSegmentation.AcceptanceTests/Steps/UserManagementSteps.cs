using System.Net.Http.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UserSegmentation.AcceptanceTests.Support;
using UserSegmentation.Application.User;
using UserSegmentation.Core.UserAggregate;
using Xunit;

namespace UserSegmentation.AcceptanceTests.Steps;

[Binding]
public class UserManagementSteps
{
  private const string UserEndpoint = "api/user";
  private readonly HttpClient _httpClient;
  private readonly ScenarioContext _context;

  public UserManagementSteps(ScenarioContext context, ApplicationFactory factory)
  {
    _context = context;
    _httpClient = factory.CreateDefaultClient(new Uri($"http://localhost/"));
  }

  [When(@"I create users with the following details")]
  public async Task WhenICreateUsersWithTheFollowingDetails(Table table)
  {
    var createUserRequests = table.CreateSet<CreateUserRequest>();
    var createdUsers = new List<CreatedUserInfo>();
    foreach (var request in createUserRequests)
    {
      var response = await _httpClient.PostAsJsonAsync(UserEndpoint, request);
      var location = response.Headers.Location?.AbsoluteUri;
      Assert.NotNull(location);
      createdUsers.Add(new CreatedUserInfo(location, request.Username, request.Email));
    }

    _context.Add("CreatedUsersLocations", createdUsers);
  }

  [Then(@"users are created successfully")]
  public async Task ThenUsersAreCreatedSuccessfully()
  {
    var createdUsersLocations = _context.Get<List<CreatedUserInfo>>("CreatedUsersLocations");
    foreach (var info in createdUsersLocations)
    {
      var response = await _httpClient.GetFromJsonAsync<UserDto>(info.Location); // return whole response
      Assert.NotNull(response);
      Assert.Equal(info.Username, response.Username);
      Assert.Equal(info.Email, response.Email);
    }
  }
  
  [When(@"I get the users list")]
  public async Task WhenIGetTheUsersList()
  {
    var result = await _httpClient.GetFromJsonAsync<List<UserDto>>(UserEndpoint);
    _context.Add("Users", result);
  }

  [Then(@"users list should not be empty")]
  public void ThenUsersListShouldNotBeEmpty()
  {
    var users = _context.Get<List<UserDto>>("Users");
    Assert.NotEmpty(users);
    Assert.Equal(3, users.Count);
  }

  [Given(@"an existing user")]
  public void GivenAnExistingUser(Table table)
  {
    var username = table.CreateInstance<UserRepresentation>();
    var testUsers = _context.Get<List<User>>("testUsers");
    var existingUser = testUsers.First(x => x.Username.Equals(username.Username));
    Assert.NotNull(existingUser);
    _context.Add("userId", existingUser.Id);
  }

  [When(@"I update his personal info with")]
  public async Task WhenIUpdateHisPersonalInfoWith(Table table)
  {
    var userId = _context.Get<int>("userId");
    var updateRequest = table.CreateInstance<UpdateUserPersonalInfoRequest>();
    _context.Add("userUpdateRequest", updateRequest);
    await _httpClient.PutAsJsonAsync($"{UserEndpoint}/{userId}", updateRequest);
  }
  
  [Then(@"personal infos are updated successfully")]
  public async Task ThenPersonalInfosAreUpdatedSuccessfully()
  {
    var userId = _context.Get<int>("userId");
    var user = await _httpClient.GetFromJsonAsync<UserDto>($"{UserEndpoint}/{userId}");
    var updateRequest = _context.Get<UpdateUserPersonalInfoRequest>("userUpdateRequest");
    Assert.NotNull(user);
    Assert.Equal(updateRequest.FirstName, user.FirstName);
    Assert.Equal(updateRequest.LastName, user.LastName);
    Assert.Equal(updateRequest.PhoneNumber, user.PhoneNumber);
  }
  private record CreatedUserInfo(string Location, string Username, string Email);

  private record UserRepresentation(string Username);
}
