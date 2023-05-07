using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UserSegmentation.AcceptanceTests.Models;
using Xunit;

namespace UserSegmentation.AcceptanceTests.Steps;

[Binding]
public class UserManagementSteps
{
  private const string UserEndpoint = "api/user";
  private readonly HttpClient _httpClient;
  private readonly ScenarioContext _context;

  public UserManagementSteps(HttpClient httpClient, ScenarioContext context)
  {
    _httpClient = httpClient;
    _context = context;
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

  private record CreatedUserInfo(string Location, string Username, string Email);
}
