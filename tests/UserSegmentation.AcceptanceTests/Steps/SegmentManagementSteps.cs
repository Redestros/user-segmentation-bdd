using System.Net.Http.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UserSegmentation.AcceptanceTests.Support;
using UserSegmentation.Application.SegmentFeature;
using Xunit;

namespace UserSegmentation.AcceptanceTests.Steps;

[Binding]
public class SegmentManagementSteps
{
  private const string SegmentEndpoint = "api/segment";
  private readonly HttpClient _httpClient;
  private readonly ScenarioContext _context;

  public SegmentManagementSteps(ScenarioContext context, ApplicationFactory factory)
  {
    _context = context;
    _httpClient = factory.CreateDefaultClient(new Uri($"http://localhost/"));
  }

  [When(@"I get the segment list")]
  public async Task WhenIGetTheSegmentList()
  {
    var result = await _httpClient.GetFromJsonAsync<List<SegmentDto>>(SegmentEndpoint);
    _context.Add("Segments", result);
  }

  [Then(@"segment list should not be empty")]
  public void ThenSegmentListShouldNotBeEmpty()
  {
    var segments = _context.Get<List<SegmentDto>>("Segments");
    Assert.NotEmpty(segments);
    Assert.Equal(4, segments.Count);
  }

  [When(@"I create segments with the following detail")]
  public async Task WhenICreateSegmentsWithTheFollowingDetail(Table table)
  {
    var createUserRequests = table.CreateSet<CreateSegmentCommand>();
    var createdUsers = new List<CreatedSegmentInfo>();
    foreach (var request in createUserRequests)
    {
      var response = await _httpClient.PostAsJsonAsync(SegmentEndpoint, request);
      var location = response.Headers.Location?.AbsoluteUri;
      Assert.NotNull(location);
      createdUsers.Add(new CreatedSegmentInfo(location, request.Name));
    }

    _context.Add("CreatedSegments", createdUsers);
  }

  [Then(@"segments are created successfully")]
  public async Task ThenSegmentsAreCreatedSuccessfully()
  {
    var createdSegmentsLocations = _context.Get<List<CreatedSegmentInfo>>("CreatedSegments");
    foreach (var info in createdSegmentsLocations)
    {
      var response = await _httpClient.GetFromJsonAsync<SegmentDto>(info.Location);
      Assert.NotNull(response);
      Assert.Equal(info.Name, response.Name);
    }
  }

  private record CreatedSegmentInfo(string Location, string Name);
}
