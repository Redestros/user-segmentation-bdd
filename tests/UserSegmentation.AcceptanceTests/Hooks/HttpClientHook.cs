using BoDi;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace UserSegmentation.AcceptanceTests.Hooks;

[Binding]
public class HttpClientHook
{
  private IObjectContainer _objectContainer;

  public HttpClientHook(IObjectContainer objectContainer)
  {
    _objectContainer = objectContainer;
  }

  [BeforeScenario()]
  public void SetupClient()
  {
    var config = LoadConfiguration();
    var apiUrl = config["Users.Api:BaseAddress"] ?? "http://localhost:8080";
    var httpClient = new HttpClient {
      BaseAddress = new Uri(apiUrl)
    };
    
    _objectContainer.RegisterInstanceAs(httpClient);
  }

  private static IConfiguration LoadConfiguration()
  {
    return new ConfigurationBuilder()
      .AddJsonFile("appsettings.json")
      .Build();
  }
}
