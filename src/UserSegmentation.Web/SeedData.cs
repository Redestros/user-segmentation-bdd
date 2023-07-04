using UserSegmentation.Application.Data;

namespace UserSegmentation.Web;

public static class SeedData
{
  public static void Initialize(IServiceProvider serviceProvider)
  {

    var dataLoader = serviceProvider.GetRequiredService<DataLoader>();

    dataLoader.LoadData();
  }
  
}
