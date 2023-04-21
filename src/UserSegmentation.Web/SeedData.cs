using Microsoft.EntityFrameworkCore;
using UserSegmentation.Infrastructure.Data;

namespace UserSegmentation.Web;

public static class SeedData
{
  public static void Initialize(IServiceProvider serviceProvider)
  {
    using var dbContext = new AppDbContext(
      serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);
    // Look for any users.


    PopulateTestData(dbContext);
  }

  public static void PopulateTestData(AppDbContext dbContext)
  {
  }
}
