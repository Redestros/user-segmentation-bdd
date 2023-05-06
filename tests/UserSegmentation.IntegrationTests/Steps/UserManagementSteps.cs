using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UserSegmentation.Application.User;
using Xunit;

namespace UserSegmentation.IntegrationTests.Steps;

[Binding]
public class UserManagementSteps
{
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
    var createdUsers = new List<CreateUserResponse>();
    foreach (var createUserRequest in createUserRequests)
    {
      var response = await _httpClient.PostAsJsonAsync("users", createUserRequest);
      var userResponse = await response.Content.ReadFromJsonAsync<CreateUserResponse>();
      createdUsers.Add(userResponse!);
    }
    _context.Add("CreatedUsers", createdUsers);
  }

  [Then(@"users are created successfully")]
  public async Task ThenUsersAreCreatedSuccessfully()
  {
    var createdUsers = _context.Get<List<CreateUserResponse>>("CreatedUsers");
    foreach (var createUser in createdUsers)
    {
      var response = await _httpClient.GetAsync($"users/{createUser.Id}"); // return whole response
      await _httpClient.GetFromJsonAsync<UserDto>($"users/{createUser.Id}"); // return specific object
      
      Assert.True(StatusCodes.Status201Created.Equals(response.StatusCode));
    }
  }
}
