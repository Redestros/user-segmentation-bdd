using System.Net.Http.Json;
using TechTalk.SpecFlow;
using UserSegmentation.AcceptanceTests.Support;
using UserSegmentation.Application.SegmentFeature;
using Xunit;

namespace UserSegmentation.AcceptanceTests.Steps;

[Binding]
public class DefaultSegmentTestsSteps
{

  private const string SegmentEndpoint = "api/segment";
  private readonly HttpClient _httpClient;
  private readonly ScenarioContext _context;

  public DefaultSegmentTestsSteps(ScenarioContext context, ApplicationFactory factory)
  {
    _context = context;
    _httpClient = factory.CreateDefaultClient(new Uri($"http://localhost/"));
  }

  [When(@"I retrieve the segment list")]
  public async Task WhenIRetrieveTheSegmentList()
  {
    var result = await _httpClient.GetFromJsonAsync<List<SegmentDto>>(SegmentEndpoint);
    _context.Add("Segments", result);
  }

  [Then(@"I should a list having one item and is the default")]
  public void ThenIShouldAListHavingOneItemAndIsTheDefault()
  {
    var segments = _context.Get<List<SegmentDto>>("Segments");
    
    var segmentsCount = segments.Count;
    Assert.Equal(1, segmentsCount);


    var firstSegment = segments.First();
    Assert.NotNull(firstSegment);
    
    Assert.Equal("default", firstSegment.Name.ToLower());
    
  }
}
